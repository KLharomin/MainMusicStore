using MainMusicStore.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        //biz burada IRepository diyerek aslında ICategoryRepository içine IRepository içinde bulunan herşeyi
        //toptan taşıdık gibi düşün.
        //Ayrıyetten buraya Category classı için başka özel metotlar girebileceğiz bunlarıda implemente edebileceğiz.

        void Update(Category category);

    }
}
