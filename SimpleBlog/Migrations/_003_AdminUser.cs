using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;
using SimpleBlog.Models; 

namespace SimpleBlog.Migrations
{
    [Migration(3)]
    public class _003_AdminUser : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("users")
                .Row(new
                {
                    Name = "admin",
                    Email = "admin@admin.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin", 13)
                });

            Insert.IntoTable("roles")
            .Row(new
            {
                Role = "admin",
            });

            string insertRoleUserQuery = "insert into roles_users select r.id, u.id from roles r, users u where u.name = 'admin' and u.name = r.role";
            Execute.Sql(insertRoleUserQuery);

        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            string deleteRoleUserQuery = "DELETE FROM roles_users WHERE (SELECT id from roles where role = 'admin')";
            Execute.Sql(deleteRoleUserQuery);

            string deleteRoleQuery = "DELETE FROM roles WHERE role = 'admin'";
            Execute.Sql(deleteRoleQuery);

            string deleteUserQuery = "DELETE FROM users WHERE name = 'admin'";
            Execute.Sql(deleteUserQuery);
        }
    }
}