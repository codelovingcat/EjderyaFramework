using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMap: EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable(@"Categories", @"dbo");
            HasKey(p => p.CategoryId);

            Property(p => p.CategoryId).HasColumnName("CategoryId");
            Property(p => p.CategoryName).HasColumnName("CategoryName");
        }
    }
}
