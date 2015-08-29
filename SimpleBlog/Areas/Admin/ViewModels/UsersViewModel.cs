using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;
using System.ComponentModel.DataAnnotations; 


namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class RoleCheckbox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string RoleName { get; set; }
    }
    
    // Format:  ControllerAction
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UsersNew
    {

        public IList<RoleCheckbox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(128), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(128), DataType(DataType.Password), Display(Name="Repeat Password")]
        public string RepeatedPassword { get; set; }
    }

    public class UsersEdit
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersChangePassword
    {
        public string Name { get; set; }

        [Required, MaxLength(128), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(128), DataType(DataType.Password), Display(Name = "Repeat Password")]
        public string RepeatedPassword { get; set; }
    }
}