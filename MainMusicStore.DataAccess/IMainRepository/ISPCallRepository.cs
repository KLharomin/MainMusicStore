using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainMusicStore.DataAccess.IMainRepository
{
    public interface ISPCallRepository:IDisposable
    {
        //IDisposible : yapacağın islemlerde garbic collectory otomatik olarak kullan demektir.
        T Single<T>(string procedureName,DynamicParameters parameters = null);
        //dapper : entityframework gibi bir orm aracıdır. Daha sağlıklıymış.
        void Execute(string procedureName, DynamicParameters parameters = null);
        //git su procedurü çalıştır şu parametreler ile çalıştır demektir.
        T OneRecord<T>(string procedureName, DynamicParameters parameters = null);
        IEnumerable<T> List<T>(string procedureName, DynamicParameters parameters = null);
        Tuple<IEnumerable<T1>,IEnumerable<T2>> List<T1,T2>(string procedureName, DynamicParameters parameters = null);
        //tuple = coklu listeleri tek bir obje gibi birleştirmeye yarar
        //dapper sqlde kullanılır sadece ve daha hafiftir.

    }
}
