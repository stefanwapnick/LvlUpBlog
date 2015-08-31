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
            get {
                    if (!HttpContext.Current.User.Identity.IsAuthenticated)
                        return null;

                    User currentUser = HttpContext.Current.Session[USER_KEY] as User;

                    // If session variable empty but ASP.NET authentication system has logged in, then current user should be set
                    if (currentUser == null)
                    {
                        currentUser = DatabaseManager.Session.Query<User>().FirstOrDefault(u => u.Name == HttpContext.Current.User.Identity.Name);
                        HttpContext.Current.Session[USER_KEY] = currentUser; 
                    }

                    return currentUser;
                }

            set { HttpContext.Current.Session[USER_KEY] = value; }
        }

        public static void ClearUser()
        {
            CurrentUser = null; 
        }
    }
}