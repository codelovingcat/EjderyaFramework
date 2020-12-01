using EjderyaFramework.Business.ValidationRules.FluentValidation;
using EjderyaFramework.Entities.Concrete;
using FluentValidation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Business.DependencyResolvers.Ninject
{
    public class ValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<Product>>().To<ProductValidatior>().InSingletonScope();
        }
    }
}
