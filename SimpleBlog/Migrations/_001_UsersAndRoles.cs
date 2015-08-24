using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data; 

namespace SimpleBlog.Migrations
{
    [Migration(1)]
    public class _001_UsersAndRoles : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("user_id").AsInt32().Identity().PrimaryKey()
                .WithColumn("username").AsString(128)
                .WithColumn("email").AsCustom("VARCHAR(256)")
                .WithColumn("password_hash").AsString(128);

            Create.Table("roles")
                .WithColumn("role_id").AsInt32().Identity().PrimaryKey()
                .WithColumn("role").AsString(128);

            Create.Table("roles_users")
                .WithColumn("user_id").AsInt32().ForeignKey("users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("roles", "role_id").OnDelete(Rule.Cascade);
        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            Delete.Table("roles_users");
            Delete.Table("roles");
            Delete.Table("users"); 
        }
    }
}