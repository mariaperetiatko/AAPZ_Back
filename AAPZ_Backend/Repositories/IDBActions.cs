using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAPZ_Backend.Repositories
{
    interface IDBActions<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetEntityList();
        IEnumerable<T> GetEntityListByClientId(int clientId);

        T GetEntity(object key);
        void Create(T item);
        void Update(T item);
        void Delete(object key);
        void Save();
    }
}
