using EjderyaFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.DataAccess
{
    /// <summary>
    /// IQueryable veri tabanından verileri geniş kapsamlı sorgulamak için kullanılır. 
    /// IQueryable LINQ for Database için yani veritabanından sorgulanan değerler için kullanılıyor. 
    /// Bu eğer veritabanı bağlantısı varsa veritabanı üzerinde SQL sorgusu yaparak değerleri seçiyor. 
    /// IQueryable arayüzü IEnumarable arayüzünü implement etmektedir. 
    /// Bu sayede IQuaryable IEnumarable özelliklerinin hepsini barındıracaktır.
    /// IQueryable kullanıldığında sorgu alınırken öncelikle filtrelendirme yapılıp sorgu gönderilir.
    /// Bu konuda performans bakımından çok iyidir.
    /// 
    /// Biz List lerle çalıştığımız zaman Context i açıp kaparız.ToList() görüldüğü yerde veritabanı sonlanmış olur..
    /// IQueryable ise o Context kapanmadan birden fazla işlem yapmak için read only bir operasyon olacaktır.
    /// Örneğin iki tane sorgu çekip arada business logic çalıştırmak gibi süreçler için IQueryabledan yararlanabiliriz.
    /// Her projede IQueryable kullanmak zorunlu değildir ama bazı projerde OData kullanımları için uygundur.
    /// Normalde diğer Repositor ler işimizi görür.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
