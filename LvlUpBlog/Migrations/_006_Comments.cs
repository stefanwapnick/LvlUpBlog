using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;
using LvlUpBlog.Models; 

namespace LvlUpBlog.Migrations
{
    [Migration(6)]
    public class _006_Comments : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            Create.Table("comments")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("content").AsCustom("TEXT")
                .WithColumn("created_at").AsDateTime()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("post_id").AsInt32().Nullable().ForeignKey("posts", "id").OnDelete(Rule.Cascade);

        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            Delete.Table("comments");
        }
    }
}