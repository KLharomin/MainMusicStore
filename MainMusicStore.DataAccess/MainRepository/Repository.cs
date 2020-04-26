using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet; //database tabloları zaten birer dbsettir.
        //database tablolarının entityframework tarafından tanınması için dbset olması gerekir.
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter); //_context.set<Category>().where(filter)
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                { //["Ali","Arda","Sefa","Cemalcan"] => split ile virgül gördüğü yerde ayırır.
                    //remove empty entries ile de boş gelen arada eğer boş gelirse onlar siler almaz.
                    query = query.Include(item);  //_context.set<Category>().include(item)
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            //tolist dersen veritabanına gider gelir onun öncesinde veritabanına gidip gelmez o yüzden en altta tolist denir.
            return query.ToList();
        }

        public T GetById(int id)
        {

            return dbSet.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter); //_context.set<Category>().where(filter)
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                { //["Ali","Arda","Sefa","Cemalcan"] => split ile virgül gördüğü yerde ayırır.
                    //remove empty entries ile de boş gelen arada eğer boş gelirse onlar siler almaz.
                    query = query.Include(item);  //_context.set<Category>().include(item)
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            T entity = dbSet.Find(id);
            Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
