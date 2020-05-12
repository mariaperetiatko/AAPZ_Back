﻿using System;
using System.Collections.Generic;
using System.Linq;
using AAPZ_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AAPZ_Backend.Repositories
{
    public class WorkplaceEquipmentRepository : IDBActions<WorkplaceEquipment>
    {
        private SheringDBContext sheringDBContext;

        public WorkplaceEquipmentRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<WorkplaceEquipment> GetEntityList()
        {
            return sheringDBContext.WorkplaceEquipment;
        }

        public IEnumerable<WorkplaceEquipment> GetEntityListByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public WorkplaceEquipment GetEntity(object id)
        {
            return sheringDBContext.WorkplaceEquipment.SingleOrDefault(x => x.Id == (int)id);
        }

        public IEnumerable<WorkplaceEquipment> GetWorkplaceEquipmentByWorkplace(int workplaceId)
        {
            return sheringDBContext.WorkplaceEquipment.Where(x => x.WorkplaceId == workplaceId);
        }

        public IEnumerable<WorkplaceEquipment> GetWorkplaceEquipmentByWorkplaceWithEquipment(int workplaceId)
        {
            return sheringDBContext.WorkplaceEquipment.Where(x => x.WorkplaceId == workplaceId).Include(y => y.Equipment);
        }

        public void Create(WorkplaceEquipment workplaceEquipment)
        {
            sheringDBContext.WorkplaceEquipment.Add(workplaceEquipment);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(WorkplaceEquipment workplaceEquipment)
        {
            sheringDBContext.Entry(workplaceEquipment).State = EntityState.Modified;
            sheringDBContext.SaveChanges();
        }

        public void Delete(object id)
        {
            WorkplaceEquipment workplaceEquipment = sheringDBContext.WorkplaceEquipment.Find(id);
            if (workplaceEquipment != null)
                sheringDBContext.WorkplaceEquipment.Remove(workplaceEquipment);
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

