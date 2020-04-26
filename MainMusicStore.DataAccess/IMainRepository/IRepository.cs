using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
        //func için giriş tipi T çıkış tipi bool demektir.
        //bilgilerin sıralı gelmesini istiyorsan orderBy tanımlaması yapılır bu şekilde.
        //Bilgi getirirken başka bir datadaki kayıtlarıda getirmek isteyebilirsin o yüzden includeProperties kullanılır.

        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
            string includeProperties = null);
        //bulduğun ilk kaydı getir ama tek kayıt geleceği için sıralama yoktur.

        void Add(T entity);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entity);
        //coklu kayıt silmek için

    }
}
