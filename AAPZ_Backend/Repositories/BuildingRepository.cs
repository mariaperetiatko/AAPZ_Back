using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AAPZ_Backend.Models;

namespace AAPZ_Backend.Repositories
{
    public class BuildingRepository : IDBActions<Building>
    {
        private SheringDBContext sheringDBContext;

        public BuildingRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<Building> GetEntityList()
        {
            return sheringDBContext.Building;
        }

        public IEnumerable<Building> GetBuildingsByLandlord(int landlordId)
        {
            return sheringDBContext.Building
                .Where(x => x.LandlordId == landlordId);
            //.Skip(skip)
            //.Take(take);
        }

        public int GetBuildingsCountByLandlord(int landlordId)
        {
            return sheringDBContext.Building.Count(x => x.LandlordId == landlordId);
        }

        public IEnumerable<Building> GetBuildingsInRadius(double maxX, double minX, double maxY, double minY)
        {
            return sheringDBContext.Building.Where(b => b.X <= maxX && b.X >= minX && b.Y <= maxY && b.Y >= minY);
        }

        public IEnumerable<Building> GetEntityListByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public Building GetEntity(object id)
        {
            return sheringDBContext.Building.SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(Building building)
        {
            sheringDBContext.Building.Add(building);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(Building building)
        {
            sheringDBContext.Entry(building).State = EntityState.Modified;
            sheringDBContext.SaveChanges();
        }

        public void Delete(object id)
        {
            Building building = sheringDBContext.Building.Find(id);
            if (building != null)
                sheringDBContext.Building.Remove(building);
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
