using EjderyaFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.DataAccess.NHibernate
{
    public class NhQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        NHiberniteHelper _nHiberniteHelper;
        private IQueryable<T> _entities;
        public NhQueryableRepository(NHiberniteHelper nHiberniteHelper)
        {
            _nHiberniteHelper = nHiberniteHelper;
        }
        public IQueryable<T> Table => this.Entities;

        protected virtual IQueryable<T> Entities
        {
            get
            {
                return _entities ?? (_entities = _nHiberniteHelper.Set<T>());

            }
        }
    }
}
