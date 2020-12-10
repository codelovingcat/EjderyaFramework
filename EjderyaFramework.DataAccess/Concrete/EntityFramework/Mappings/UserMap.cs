using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable(@"Users", @"dbo");
            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.UserName).HasColumnName("UserName");
            Property(p => p.Email).HasColumnName("Email");
            Property(p => p.FirstName).HasColumnName("FirstName");
            Property(p => p.LastName).HasColumnName("LastName");
            Property(p => p.Password).HasColumnName("Password");
        }
    }
}
