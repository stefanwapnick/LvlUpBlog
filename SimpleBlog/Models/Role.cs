using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string RoleName { get; set; }
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            // Indicate which table to use
            Table("roles");

            // Indicate which property is id and indicate that auto-incremented
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            // Add mappings
            Property(x => x.RoleName, x => { x.Column("role"); x.NotNullable(true); });
        }

    }

}