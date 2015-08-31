using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace LvlUpBlog.Models
{
    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual string Content {get; set;}
        public virtual DateTime DateCreated {get; set;}
        public virtual User User {get; set;}
    }

    public class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Table("comments");
            Id(x => x.Id, x => { x.Column("id"); x.Generator(Generators.Identity); });
            Property(x => x.Content, x => { x.Column("content"); x.NotNullable(true); });
            Property(x => x.DateCreated, x => { x.Column("created_at"); x.NotNullable(true); });

            
            ManyToOne(x => x.User, x =>
            {
                x.Column("user_id");
            });
            
        }
    }
}