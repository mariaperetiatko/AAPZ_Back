using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AAPZ_Backend.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace AAPZ_Backend.Repositories
{
    public class MonitoringRepository
    {
        private SheringDBContext sheringDBContext;

        public MonitoringRepository()
        {
            this.sheringDBContext = new SheringDBContext();
        }

        public IEnumerable<Monitoring> GetList(int clientId)
        {
            return sheringDBContext.Monitoring.Where(y => y.ClientId == clientId);
        }

        public Monitoring GetByDate(DateTime date, int clientId)
        {
            return sheringDBContext.Monitoring.FirstOrDefault(y => y.ClientId == clientId
                                                                   && y.Date.Year == date.Year
                                                                   && y.Date.Month == date.Month
                                                                   && y.Date.Day == date.Day);
        }

        public IEnumerable<Monitoring> GetLastMonitorings(int clientId)
        {
            return sheringDBContext.Monitoring
                .Where(y => y.ClientId == clientId)
                .OrderByDescending(x => x.Date)
                .Take(5);
        }

        public void GenerateMonitoring(int clientId)
        {
            DateTime curDate = DateTime.Now;
            List<DateTime> dates = (from p in sheringDBContext.Diastance
                                  
                                      select new DateTime(p.Date.Year, p.Date.Month,p.Date.Day)).Distinct().ToList();

            //IEnumerable<DateTime> dates = sheringDBContext.Diastance
            //    .Where(y => y.Date.Year != DateTime.Now.Year
            //                && y.Date.Month != DateTime.Now.Month
            //                && y.Date.Day != DateTime.Now.Day)
            //    .Select(d => new {d.Date.Year, d.Date.Month, d.Date.Day})
            //    .Distinct()
            //    .ToList()
            //    .Select(x => new DateTime(x.Year, x.Month, x.Day));

            foreach (var date in dates)
            {
                var mon = sheringDBContext.Monitoring
                    .FirstOrDefault(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);

                if (mon == null)
                {
                    int rightCount = sheringDBContext.Diastance
                        .Count(x => x.DistanceValue >= 50 && x.DistanceValue <= 90);

                    int count = sheringDBContext.Diastance.Count();

                    double rightValues = 0;

                    if (count != 0)
                    {
                        rightValues = (double) ((double) rightCount / count);
                    }

                    Monitoring monitoring = new Monitoring
                    {
                        ClientId = clientId,
                        Date = date,
                        RightValues = rightValues
                    };

                    IEnumerable<Distance> distances = sheringDBContext.Diastance
                        .Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);

                    sheringDBContext.Diastance.RemoveRange(distances);
                    sheringDBContext.SaveChanges();
                    sheringDBContext.Monitoring.Add(monitoring);
                    sheringDBContext.SaveChanges();
                }
            }
        }
    }
}
