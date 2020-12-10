using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Business.Concrete.Manager;
using EjderyaFramework.Core.DataAccess;
using EjderyaFramework.Core.DataAccess.EntityFramework;
using EjderyaFramework.Core.DataAccess.NHibernate;
using EjderyaFramework.DataAccess.Abstract;
using EjderyaFramework.DataAccess.Concrete.EntityFramework;
using EjderyaFramework.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();


            Bind<IUserService>().To<UserManager>();
            Bind<IUserDal>().To<EfUserDal>();


            ///<summary>
            ///IQuerable için eğer kullanmak istersek alt yapısını burada bind ediyoruz.
            /// </summary>
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<NorthwindContext>();
            ///<summary>
            ///NHibernate için
            /// </summary>
            Bind<NHibernateHelper>().To<SqlServerHelper>();
        }
    }
}
