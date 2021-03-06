﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AAPZ_Backend.Models;


namespace AAPZ_Backend.Repositories
{
    public class WorkplaceRepository : IDBActions<Workplace>
    {
        private SheringDBContext sheringDBContext;

        public WorkplaceRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<Workplace> GetEntityList()
        {
            return sheringDBContext.Workplace;
        }

        public IEnumerable<Workplace> GetEntityListByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Workplace GetEntity(object id)
        {
            return sheringDBContext.Workplace
                .Include(x => x.WorkplaceEquipment)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.Building)
                .ThenInclude(y => y.Landlord)
                .SingleOrDefault(x => x.Id == (int) id);
        }

        public IEnumerable<Workplace> GetPagedWorkplacesByBuildingId(int buildingId, int skip, int take)
        {

            return sheringDBContext.Workplace
                .Where(x => x.BuildingId == buildingId)
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Include(x => x.WorkplaceEquipment)
                .ThenInclude(x => x.Equipment);
        }


        public int GetWorkplacesByBuildingIdCount(int buildingId)
        {
            return sheringDBContext.Workplace
                .Count(x => x.BuildingId == buildingId);
        }

        public IEnumerable<Workplace> GetWorkplacesByBuildingId(int buildingId)
        {
            return sheringDBContext.Workplace.Where(x => x.BuildingId == buildingId);
        }

        public void Create(Workplace workplace)
        {
            sheringDBContext.Workplace.Add(workplace);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(Workplace workplace)
        {
            sheringDBContext.Entry(workplace).State = EntityState.Modified;
            sheringDBContext.SaveChanges();
        }

        public void Delete(object id)
        {
            Workplace workplace = sheringDBContext.Workplace.Find(id);
            if (workplace != null)
                sheringDBContext.Workplace.Remove(workplace);
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
