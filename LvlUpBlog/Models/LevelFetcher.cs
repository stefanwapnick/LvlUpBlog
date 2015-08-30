using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace LvlUpBlog.Models
{
    public static class LevelFetcher
    {
        /// <summary>
        /// Returns the current entity level for the site or user
        /// </summary>
        /// <param name="currentAmount">current amount of user or site posts</param>
        /// <param name="forSite">If True, returns the level for the site. Else returns level for user</param>
        /// <returns>Level entity representing level of site or user</returns>
        public static Level getLevel(int currentAmount, bool forSite = true)
        {
            string typeString = "site";

            if (!forSite)
                typeString = "user"; 

            
            Level ret = DatabaseManager.Session.Query<Level>()
                                .OrderBy(x => x.AmountToNext)
                                .Where(x => x.LevelType == typeString && x.AmountToNext > currentAmount)
                                .FirstOrDefault();
            
            return ret; 
        }


    }
}