using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;
using LvlUpBlog.Models; 

namespace LvlUpBlog.Migrations
{
    [Migration(4)]
    public class _004_Levels : Migration
    {
        /// <summary>
        /// Migrate to next schema version
        /// </summary>
        public override void Up()
        {
            Create.Table("levels")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("tier").AsInt32()
                .WithColumn("name").AsString(128)
                .WithColumn("amount_to_next").AsInt32()
                .WithColumn("image_path").AsCustom("VARCHAR(256)")
                .WithColumn("level_type").AsString(128);

        }

        /// <summary>
        /// Rollback to previous database schema
        /// </summary>
        public override void Down()
        {
            Delete.Table("levels");
        }
    }
}