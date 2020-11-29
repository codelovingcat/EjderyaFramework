using EjderyaFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.DataAccess.NHibernate
{
    public class NhEntityRepositorBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private NHiberniteHelper _nHiberniteHelper;
        public NhEntityRepositorBase(NHiberniteHelper nHiberniteHelper)
        {
            _nHiberniteHelper = nHiberniteHelper;
        }

        public TEntity Add(TEntity entity)
        {
            using (var session=_nHiberniteHelper.OpenSession())
            {
                session.Save(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = _nHiberniteHelper.OpenSession())
            {
                session.Delete(entity);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var session = _nHiberniteHelper.OpenSession())
            {
                return session.Query<TEntity>().FirstOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var session = _nHiberniteHelper.OpenSession())
            {
                return filter==null
                    ? session.Query<TEntity>().ToList()
                    : session.Query<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = _nHiberniteHelper.OpenSession())
            {
                session.Update(entity);
                return entity;
            }
        }
    }
}
