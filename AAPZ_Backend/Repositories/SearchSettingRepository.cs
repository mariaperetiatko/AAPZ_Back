using System;
using System.Collections.Generic;
using System.Linq;
using AAPZ_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AAPZ_Backend.Repositories
{
    public class SearchSettingRepository : IDBActions<SearchSetting>
    {
        private SheringDBContext sheringDBContext;

        public SearchSettingRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<SearchSetting> GetEntityList()
        {
            return sheringDBContext.SearchSetting;
        }

        public IEnumerable<SearchSetting> GetEntityListByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public SearchSetting GetEntity(object id)
        {
            return sheringDBContext.SearchSetting.SingleOrDefault(x => x.SearchSettingId == (int)id);
        }

        public void Create(SearchSetting searchSetting)
        {
            sheringDBContext.SearchSetting.Add(searchSetting);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(SearchSetting searchSetting)
        {
            sheringDBContext.Entry(searchSetting).State = EntityState.Modified;
            sheringDBContext.SaveChanges();
        }

        public void Delete(object id)
        {
            SearchSetting searchSetting = sheringDBContext.SearchSetting.Find(id);
            if (searchSetting != null)
                sheringDBContext.SearchSetting.Remove(searchSetting);
            sheringDBContext.SaveChanges();
        }

        public void Save()
        {
            sheringDBContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    sheringDBContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}