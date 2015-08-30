using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;
using LvlUpBlog.Models; 

namespace LvlUpBlog.Migrations
{
    [Migration(5)]
    public class _005_InitializeLevels : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            string insertSql = @"INSERT INTO levels (tier, name, amount_to_next, image_path, level_type) 
                               VALUES (1, 'Blog Chicklet', 5, '~/content/images/placeholder.png', 'user'), 
                                      (2, 'Blog Hound', 20, '~/content/images/placeholder.png', 'user'),
                                      (3, 'Blog Star', 50, '~/content/images/placeholder.png', 'user'),
                                      (1, 'Blog Shack', 20, '~/content/images/placeholder.png', 'site'),
                                      (2, 'Blog Castle', 50, '~/content/images/placeholder.png', 'site'),
                                      (3, 'Blog Nation', 100, '~/content/images/placeholder.png', 'site')";
            Execute.Sql(insertSql); 
        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            string deleteSql = "DELETE FROM levels";
            Execute.Sql(deleteSql); 
        }
    }
}