using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvlUpBlog.Models
{
    public class Tag
    {

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Slug { get; set; }

        public virtual IList<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }

    }

    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("tags");

            Id(x => x.Id, x => { x.Column("id"); x.Generator(Generators.Identity); });

            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));

            Bag(x => x.Posts, x =>
            {
                x.Key(k => k.Column("tag_id"));
                x.Table("posts_tags"); 
            }, x=> x.ManyToMany(k => k.Column("post_id") )); 

        }
    }
}