using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Models;
using NHibernate.Linq;
using SimpleBlog.Infrastructure;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View(new UsersIndex(){
                Users = DatabaseManager.Session.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew(){

                // Select all roles from database. Create a List<Role> Roles containing this information in addition to isChecked = false
                Roles = DatabaseManager.Session.Query<Role>().Select(role => new RoleCheckbox 
                { 
                    Id = role.Id, 
                    IsChecked = false,
                    RoleName = role.RoleName
                }).ToList() 
                
            }); 
        }

        [HttpPost]
        public ActionResult New(UsersNew model)
        {
            User newUser = new User();
            SyncRoles(model.Roles, newUser.Roles); 

            // Validate user input to create new user 
            //-----------------------------------------------------------
            if (DatabaseManager.Session.Query<User>().Any(u => u.Name == model.Name))
                ModelState.AddModelError("Name", "That user already exists");

            if (model.RepeatedPassword != model.Password)
            {
                ModelState.Clear(); 
                ModelState.AddModelError("Password", "Password fields do not match");
                model.Password = "";
                model.RepeatedPassword = ""; 
            }
                

            if (!ModelState.IsValid)
                return View(model);

            // Create a new user
            //-----------------------------------------------------------
            newUser.Email = model.Email;
            newUser.Name = model.Name;
            newUser.Password = HashUtility.HashPassword(model.Password);

            DatabaseManager.Session.Save(newUser); 

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            User selectedUser = DatabaseManager.Session.Load<User>(id);
            if (selectedUser == null)
                return HttpNotFound();

            return View(new UsersEdit(){
                Name = selectedUser.Name, 
                Email = selectedUser.Email,

                // Get list of all roles and check if contained in user
                Roles = DatabaseManager.Session.Query<Role>().Select(role => new RoleCheckbox 
                { 
                    Id = role.Id, 
                    IsChecked = selectedUser.Roles.Contains(role),
                    RoleName = role.RoleName
                }).ToList() 
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, UsersEdit model)
        {
            // Load user of given id from database
            User selectedUser = DatabaseManager.Session.Load<User>(id);
            if (selectedUser == null)
                return HttpNotFound();

            SyncRoles(model.Roles, selectedUser.Roles); 

            // Check if name entered belongs to another user other than self 
            if(DatabaseManager.Session.Query<User>().Any(x => x.Name == model.Name && x.Id != id))
                ModelState.AddModelError("Name", "Username entered belongs to another user."); 
            
            if(!ModelState.IsValid)
                return View(model);

            selectedUser.Name = model.Name;
            selectedUser.Email = model.Email;

            DatabaseManager.Session.Update(selectedUser);

            return RedirectToAction("Index");  

        }

        public ActionResult ChangePassword(int id)
        {
            // Load user of given id from database
            User selectedUser = DatabaseManager.Session.Load<User>(id);
            if (selectedUser == null)
                return HttpNotFound();

            return View(new UsersChangePassword(){Name = selectedUser.Name});
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, UsersChangePassword model)
        {
            // Load user of given id from database
            User selectedUser = DatabaseManager.Session.Load<User>(id);
            if (selectedUser == null)
                return HttpNotFound();

            // Check if repeat password and password field match
            if (model.RepeatedPassword != model.Password)
            {
                ModelState.Clear(); 
                ModelState.AddModelError("Password", "Password fields do not match!");
                model.RepeatedPassword = "";
                model.Password = ""; 
            }

            if(!ModelState.IsValid)
                return View(model);

            selectedUser.Password = HashUtility.HashPassword(model.Password);

            DatabaseManager.Session.Update(selectedUser);

            return RedirectToAction("index");  
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Load user of given id from database
            User selectedUser = DatabaseManager.Session.Load<User>(id);
            if (selectedUser == null)
                return HttpNotFound();

            DatabaseManager.Session.Delete(selectedUser);
            return RedirectToAction("index"); 

        }

        /// <summary>
        /// Syncs selected roles in front-end to user roles in database
        /// </summary>
        /// <param name="checkboxes">Roles selected in frontend</param>
        /// <param name="roles">Roles already assigned to user</param>
        private void SyncRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {

            List<Role> selectedRoles = new List<Role>();

            foreach (Role role in DatabaseManager.Session.Query<Role>().ToList())
            {
                // Get the checkbox that matches the role id
                RoleCheckbox checkbox = checkboxes.Single(c => c.Id == role.Id);
                
                if (checkbox.IsChecked)
                {
                    checkbox.RoleName = role.RoleName;
                    selectedRoles.Add(role); 
                }
            }

            // Add roles which were selected and not contained already in user roles
            foreach (var toAdd in selectedRoles.Where(r => !roles.Contains(r)).ToList())
                roles.Add(toAdd);

            // Remove roles which were de-selected
            foreach (var toRemove in roles.Where(r => !selectedRoles.Contains(r)).ToList())
                roles.Remove(toRemove); 
        }

    }
}