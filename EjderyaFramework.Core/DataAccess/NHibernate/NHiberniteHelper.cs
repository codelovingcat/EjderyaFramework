using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.DataAccess.NHibernate
{
    /// <summary>
    /// IDisposable Design Pattern ini implemente ediyoruz NHibernate için Sql helper class yazıp entegre etmemiz lazım.
    /// private static ISessionFactory _sessionFactory; ismindende anlaşılacağı gibi Factory tasarım deseninden besleniyor.
    /// Oracle, Sql vs veritabanlarına hangi veri tabanını gönderirsek onun konfigürasyonunu gönderiyor.
    /// virtual birnesneden yola çıkarak ilişkili nesnelerini getirmek gibi virtual a ihtiyaç duyarız. 
    /// </summary>
    public abstract class NHiberniteHelper : IDisposable
    {
        private static ISessionFactory _sessionFactory;
        public virtual ISessionFactory SessionFactory
        {
            get
            {
              return  _sessionFactory ?? (_sessionFactory = InitializeFactory());
            }
        }

        protected abstract ISessionFactory InitializeFactory();

        public virtual ISession OpenSession() 
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
