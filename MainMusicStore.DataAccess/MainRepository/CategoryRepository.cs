using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //Burda aslında repository irepositoryden tüm kayıtları alıyor bu yüzden bu kayıtlar direk olarak içine geliyor.
        //ICategory den özel kayıt varsa o gelecek onlar buraya yazılacaktır.
        //ICategory içinden gelen IRepository bilgilerini ise Reposiyory<Category> doldurmaktadır.
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // base de ki context yani bu contextin ana classdan geleceğini söylüyor bunun ana classı ise repository
        //içindeki alandan gelir.

        public void Update(Category category)
        {
            var data = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (data!=null)
            {
                data.Name = category.Name;
            }
            _context.SaveChanges();
        }

    }
}
