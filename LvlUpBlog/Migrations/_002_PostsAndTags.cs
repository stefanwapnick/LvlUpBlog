using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data; 

namespace LvlUpBlog.Migrations
{
    [Migration(2)]
    public class _002_PostsAndTags : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            Create.Table("posts")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("title").AsString(128)
                .WithColumn("slug").AsString(128)
                .WithColumn("content").AsCustom("TEXT")
                .WithColumn("created_at").AsDateTime()
                .WithColumn("updated_at").AsDateTime().Nullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            Create.Table("tags")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("slug").AsString(128)
                .WithColumn("name").AsString(128);

            Create.Table("posts_tags")
                .WithColumn("post_id").AsInt32().ForeignKey("posts", "id").OnDelete(Rule.Cascade)
                .WithColumn("tag_id").AsInt32().ForeignKey("tags", "id").OnDelete(Rule.Cascade); 
        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            Delete.Table("posts_tags");
            Delete.Table("posts");
            Delete.Table("tags"); 
        }
    }
}