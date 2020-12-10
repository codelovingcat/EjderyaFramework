using EjderyaFramework.Core.DataAccess;
using EjderyaFramework.Entities.ComplexTypes;
using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        List<UserRoleItem> GetUserRoles(User user);
    }
}
