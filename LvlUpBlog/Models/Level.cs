using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace LvlUpBlog.Models
{
    public class Level
    {
        public virtual int Id { get; set; }
        public virtual int Tier { get; set; }
        public virtual string Name { get; set; }
        public virtual int AmountToNext { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string LevelType { get; set; }
    }

    public class LevelMap : ClassMapping<Level>
    {
        public LevelMap()
        {
            Table("levels"); 

            Id(x => x.Id, x => { x.Column("id"); x.Generator(Generators.Identity); });

            Property(x => x.Tier, x => { x.Column("tier"); x.NotNullable(true); });
            Property(x => x.Name, x => { x.Column("name"); x.NotNullable(true); });
            Property(x => x.AmountToNext, x => { x.Column("amount_to_next"); x.NotNullable(true); });
            Property(x => x.ImagePath, x => { x.Column("image_path"); x.NotNullable(true); });
            Property(x => x.LevelType, x => { x.Column("level_type"); x.NotNullable(true); });
        }
    }
}