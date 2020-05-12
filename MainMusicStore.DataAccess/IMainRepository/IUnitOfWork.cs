using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork:IDisposable
    {
        //Unit of workün kontrolüde olmasını istediğin şeyler nelerse onları yazarsın burada.
        ICategoryRepository Category { get; }
        ISPCallRepository SPCall { get; }
        ICoverTypeRepository CoverType { get; }
        void Save();
    }
}
