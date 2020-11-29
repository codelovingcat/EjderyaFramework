using EjderyaFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.DataAccess.EntityFramework
{
    /// <summary>
    ///  public IQueryable<T> Table {get;} bir tabloya abone olup o tablo üzerinde Queryable yapmamızı sağlayacak.
    ///  protected virtual IDbSet<T> Entities burada protected olma sebebi EfQueryableRepository birisi newlediği zaman
    ///  bunu kimse kullanamasın diyedir.virtual ise Lazy Loading yapabilsin diye gerekli olan imzadır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _entities;
        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table => this.Entities;

        protected virtual IDbSet<T> Entities
        {
            get 
            {
                return _entities ?? (_entities = _context.Set<T>());

                //if (_entities==null)
                //{
                //    _entities = _context.Set<T>();
                //}
                //return _entities;
            }
        }
    }
}
