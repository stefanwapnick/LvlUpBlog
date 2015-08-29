using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace LvlUpBlog.Models
{
    /// <summary>
    /// Stores cache of currently logged in user
    /// </summary>
    public static class UserCache
    {
        private static string USER_KEY = "UserCacheKeyConstant";

        public static User CurrentUser
        {
            /*
            get {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null; 

                User currentUser = HttpContext.Current.Items[USER_KEY] as User;

                if (currentUser == null)
                {
                    currentUser = DatabaseManager.Session.Query<User>().FirstOrDefault(u => u.Name == HttpContext.Current.User.Identity.Name);

                    if (currentUser == null)
                        return null;

                    HttpContext.Current.Items[USER_KEY] = currentUser; 
                }

                return currentUser; 
            }*/

            get { return HttpContext.Current.Session[USER_KEY] as User; }
            set { HttpContext.Current.Session[USER_KEY] = value; }
        }

        public static void ClearUser()
        {
            CurrentUser = null; 
        }
    }
}