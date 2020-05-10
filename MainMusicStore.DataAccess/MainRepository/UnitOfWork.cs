using MainMusicStore.Data;
using MainMusicStore.DataAccess.IMainRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            //Category repository ve spcallrepository direk olarak _db nesnesi ister o yüzden verildi.
            SPCall = new SPCallRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public ISPCallRepository SPCall { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
            //veritabanı ile işin bittiği anda at çöpe demek.
        }

        public void Save()
        {
            _db.SaveChanges();
            //category ve spcall için save yazmak gerekmez zaten bunu burası yapacak
        }
    }
}
