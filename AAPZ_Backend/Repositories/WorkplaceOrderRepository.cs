using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AAPZ_Backend.Models;

namespace AAPZ_Backend.Repositories
{
    public class WorkplaceOrderRepository : IDBActions<WorkplaceOrder>
    {
        private SheringDBContext sheringDBContext;

        public WorkplaceOrderRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<WorkplaceOrder> GetEntityList()
        {
            return sheringDBContext.WorkplaceOrder;
        }

        public IEnumerable<WorkplaceOrder> GetEntityListByClientId(int clientId)
        {
            return sheringDBContext.WorkplaceOrder.Where(x => x.ClientId == clientId);
        }

        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByWorkplaceId(int workplaceId)
        {
            return sheringDBContext.WorkplaceOrder.Where(x => x.WorkplaceId == workplaceId);
        }

        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByWorkplaceAndDate(int workplaceId, DateTime date)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.StartTime.Year == date.Year
                        && x.StartTime.Month == date.Month
                        && x.StartTime.Day == date.Day
                        && x.WorkplaceId == workplaceId).OrderBy(x => x.StartTime);
        }


        public IEnumerable<WorkplaceOrder> GetPreviousWorkplaceOrdersByClient(DateTime date, int clientId, int skip, int take)
        {

            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && x.FinishTime <= DateTime.Now)
                .OrderBy(x => x.StartTime)
                .Skip(skip)
                .Take(take)
                .Include(x => x.Workplace)
                .ThenInclude(x => x.Building);
        }



        public int GetPreviousWorkplaceOrdersByClientCount(DateTime date, int clientId)
        {
            return sheringDBContext.WorkplaceOrder
                .Count(x => x.Client.Id == clientId
                            && x.FinishTime <= DateTime.Now);
        }

        public IEnumerable<WorkplaceOrder> GetPreviousWorkplaceOrdersByWorkplace(long workplaceId)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.WorkplaceId == workplaceId
                            && x.StartTime <= DateTime.Now)
                .Include(x => x.Client);
        }

        public IEnumerable<WorkplaceOrder> GetFutureWorkplaceOrdersByWorkplace(long workplaceId)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.WorkplaceId == workplaceId
                            && x.StartTime > DateTime.Now)
                .Include(x => x.Client);
        }

        public IEnumerable<WorkplaceOrder> GetFutureWorkplaceOrdersByClient(DateTime date, int clientId, int skip, int take, string like)
        {
            if (String.IsNullOrEmpty(like))
            {
                return sheringDBContext.WorkplaceOrder
                    .Where(x => x.Client.Id == clientId
                                && x.StartTime >= DateTime.Now)
                    .OrderBy(x => x.StartTime)
                    .Skip(skip)
                    .Take(take)
                    .Include(x => x.Workplace)
                    .ThenInclude(x => x.Building);
            }

            List<int> buildingIds = sheringDBContext.Building.Where(x =>
                x.Name.StartsWith(like, StringComparison.OrdinalIgnoreCase))
               .Select(x => x.Id).ToList();

            List<int> workplaceIds = sheringDBContext.Workplace.Where(y =>
                buildingIds.Contains(y.BuildingId)).Select(y => y.Id).ToList();

            return sheringDBContext.WorkplaceOrder
            .Where(x => x.Client.Id == clientId
                        && workplaceIds.Contains(x.WorkplaceId)
                        && x.StartTime >= DateTime.Now)
                .OrderBy(x => x.StartTime)
                .Skip(skip)
                .Take(take)
                .Include(x => x.Workplace)
                .ThenInclude(x => x.Building);
        }

        public int GetFutureWorkplaceOrdersByClientCount(DateTime date, int clientId, string like)
        {
            if (String.IsNullOrEmpty(like))
            {
                return sheringDBContext.WorkplaceOrder
                    .Count(x => x.Client.Id == clientId
                                && x.StartTime >= DateTime.Now);
            }

            List<int> buildingIds = sheringDBContext.Building.Where(x =>
                x.Name.StartsWith(like, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id).ToList();

            List<int> workplaceIds = sheringDBContext.Workplace.Where(y =>
                buildingIds.Contains(y.BuildingId)).Select(y => y.Id).ToList();

            return sheringDBContext.WorkplaceOrder
                .Count(x => x.Client.Id == clientId
                            && workplaceIds.Contains(x.WorkplaceId)
                            && x.StartTime >= DateTime.Now);
        }

        public IEnumerable<WorkplaceOrder> GetFilteredWorkplaceOrdersByClient(DateTime startTime, DateTime finishTime,
            int clientId, int skip, int take, string like)
        {
            if (String.IsNullOrEmpty(like))
            {
                return sheringDBContext.WorkplaceOrder
                    .Where(x => x.Client.Id == clientId
                                && x.StartTime >= startTime
                                && x.FinishTime <= finishTime)
                    .OrderBy(x => x.StartTime)
                    .Skip(skip)
                    .Take(take)
                    .Include(x => x.Workplace)
                    .ThenInclude(x => x.Building);
            }

            List<int> buildingIds = sheringDBContext.Building.Where(x =>
                x.Name.StartsWith(like, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id).ToList();

            List<int> workplaceIds = sheringDBContext.Workplace.Where(y =>
                buildingIds.Contains(y.BuildingId)).Select(y => y.Id).ToList();

            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && workplaceIds.Contains(x.WorkplaceId)
                            && x.StartTime >= startTime
                            && x.FinishTime <= finishTime)
                .Include(x => x.Workplace)
                .ThenInclude(x => x.Building)
                .OrderBy(x => x.StartTime)
                .Skip(skip)
                .Take(take);
        }


        public IEnumerable<WorkplaceOrder> GetFilteredWorkplaceOrdersByClient(DateTime startTime, DateTime finishTime, int clientId)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && x.StartTime >= startTime
                            && x.FinishTime <= finishTime);
        }

        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByClientAndYear(int year, int clientId)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && x.StartTime.Year == year);
        }

        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByClientAndMonth(int year, int month, int clientId)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && x.StartTime.Year == year
                            && x.StartTime.Month == month);
        }

        public int GetFilteredWorkplaceOrdersByClientCount(DateTime startTime, DateTime finishTime, int clientId, string like)
        {
            if (String.IsNullOrEmpty(like))
            {
                return sheringDBContext.WorkplaceOrder
                    .Count(x => x.Client.Id == clientId
                                && x.StartTime >= startTime
                                && x.FinishTime <= finishTime);
            }

            List<int> buildingIds = sheringDBContext.Building.Where(x =>
                x.Name.StartsWith(like, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Id).ToList();

            List<int> workplaceIds = sheringDBContext.Workplace.Where(y =>
                buildingIds.Contains(y.BuildingId)).Select(y => y.Id).ToList();

            return sheringDBContext.WorkplaceOrder
                .Count(x => x.Client.Id == clientId
                            && workplaceIds.Contains(x.WorkplaceId)
                            && x.StartTime >= startTime
                            && x.FinishTime <= finishTime);
        }

        public IEnumerable<WorkplaceOrder> GetCurrentWorkplaceOrdersByClient(int clientId, int skip, int take)
        {
            return sheringDBContext.WorkplaceOrder
                .Where(x => x.Client.Id == clientId
                            && x.StartTime <= DateTime.Now
                            && x.FinishTime >= DateTime.Now)
                .OrderBy(x => x.StartTime)
                .Skip(skip)
                .Take(take)
                .Include(x => x.Workplace)
                .ThenInclude(x => x.Building);
        }

        public int GetCurrentWorkplaceOrdersByClientCount(int clientId)
        {
            return sheringDBContext.WorkplaceOrder
                .Count(x => x.Client.Id == clientId
                            && x.StartTime <= DateTime.Now
                            && x.FinishTime >= DateTime.Now);
        }

        public WorkplaceOrder GetEntity(object id)
        {
            return sheringDBContext.WorkplaceOrder
                .Include(x => x.Workplace)
                .ThenInclude(x => x.WorkplaceEquipment)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.Workplace)
                .ThenInclude(y => y.Building)
                .ThenInclude(x => x.Landlord)
                .SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(WorkplaceOrder workplaceOrder)
        {
            Workplace workplace = sheringDBContext.Workplace.FirstOrDefault(e => e.Id == workplaceOrder.WorkplaceId);

            sheringDBContext.WorkplaceOrder.Add(workplaceOrder);
            sheringDBContext.SaveChanges();
        }

        //public Client GetClient(string tokenId)
        //{
        //    Client client = sheringDBContext.Client.Where(x => x.IdentityId == tokenId).FirstOrDefault();
        //    return client;
        //}

        public void Update(WorkplaceOrder workplaceOrder)
        {
            sheringDBContext.Entry(workplaceOrder).State = EntityState.Modified;
            sheringDBContext.SaveChanges();
        }

        public void Delete(object id)
        {
            WorkplaceOrder workplaceOrder = sheringDBContext.WorkplaceOrder.Find(id);
            if (workplaceOrder != null)
                sheringDBContext.WorkplaceOrder.Remove(workplaceOrder);
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
