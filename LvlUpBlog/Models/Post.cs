using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvlUpBlog.Models
{
    public class Post
    {

        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Title { get; set; }
      
        public virtual string Slug { get; set; }
        public virtual string Content { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }

        public virtual bool IsDeleted { get { return DeletedAt != null; } }

        public virtual IList<Tag> Tags { get; set;  }
        public virtual IList<Comment> Comments { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>(); 
        }
    }

    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("posts"); 

            Id(x => x.Id, x => {x.Column("id"); x.Generator(Generators.Identity); }); 

            Property(x => x.Title, x => { x.Column("title"); x.NotNullable(true); }); 
            Property(x => x.Slug, x => { x.Column("slug"); x.NotNullable(true); });
            Property(x => x.Content, x => { x.Column("content"); x.NotNullable(true); });
            Property(x => x.Title, x => { x.Column("title"); x.NotNullable(true); });

            Property(x => x.CreatedAt, x => { x.Column("created_at"); x.NotNullable(true); });
            Property(x => x.UpdatedAt, x => { x.Column("updated_at"); });
            Property(x => x.DeletedAt, x => { x.Column("deleted_at"); });

            // Many to one mapping for Posts to User
            ManyToOne(x => x.User, x =>
            {
                x.Column("user_id");
                x.NotNullable(true); 
            });


            // One to Many mapping for comments
            Bag(x => x.Comments, x =>
            {
                x.Inverse(false);                              // Indicates that this is the owner of the association (foreign key in child table)
                x.Cascade(Cascade.All);
                x.Key(k => k.Column(c => c.Name("post_id")));  // Column name which contains foreign key in comments table
            }, x => x.OneToMany()); 


            // Many to Many mapping for Posts to Tags
            Bag(x => x.Tags, x =>
            {
                x.Key(k => k.Column("post_id"));
                x.Table("posts_tags");
            }, x => x.ManyToMany(k => k.Column("tag_id"))); 

        }
    }
}