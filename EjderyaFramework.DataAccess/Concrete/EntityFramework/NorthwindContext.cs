using EjderyaFramework.DataAccess.Concrete.EntityFramework.Mappings;
using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        /// <summary>
        /// Burada NorthwindContext() database Initializer etmekteki amaç 
        /// Migration u kapatmak otomatik birşey oluşturmaya çalışmasın 
        /// ben zaten var olan bir database kullanacağım demek.
        /// </summary>
        public NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<Role> Roles{ get; set; }

        //

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            //
        }
    }
}
