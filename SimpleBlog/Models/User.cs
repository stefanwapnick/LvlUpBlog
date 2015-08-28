using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        public virtual IList<Role> Roles { get; set;  }

        public User()
        {
            Roles = new List<Role>(); 
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            // Indicate which table to use
            Table("users");
           
            // Indicate which property is id and indicate that auto-incremented
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            // Add mappings
            Property(x => x.Name, x => { x.Column("name"); x.NotNullable(true); });
            Property(x => x.Email, x => { x.Column("email"); x.NotNullable(true); });
            Property(x => x.Password, x => { x.Column("password"); x.NotNullable(true); });

            Bag(x => x.Roles, x =>
                {
                    x.Table("roles_users");
                    x.Key(k => k.Column("user_id"));
                }, x=> x.ManyToMany(k => k.Column("role_id"))); 

        }    
        
    }

}