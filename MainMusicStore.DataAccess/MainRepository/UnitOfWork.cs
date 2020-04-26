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
        }
    }
}
