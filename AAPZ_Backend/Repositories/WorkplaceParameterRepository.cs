using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAPZ_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AAPZ_Backend.Repositories
{
    public class WorkplaceParameterRepository : IDBActions<WorkplaceParameter>
    {
        private SheringDBContext sheringDBContext;

        public WorkplaceParameterRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<WorkplaceParameter> GetEntityList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkplaceParameter> GetEntityListByClientId(int clientId)
        {
            return sheringDBContext.WorkplaceParameter.Where(b => b.ClientId == clientId);
        }

        public WorkplaceParameter GetEntity(object id)
        {
            return sheringDBContext.WorkplaceParameter.SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(WorkplaceParameter workplace)
        {
            sheringDBContext.WorkplaceParameter.Add(workplace);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(WorkplaceParameter workplace)
        {
            sheringDBContext.Entry(workplace).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            WorkplaceParameter workplace = sheringDBContext.WorkplaceParameter.Find(id);
            if (workplace != null)
                sheringDBContext.WorkplaceParameter.Remove(workplace);
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