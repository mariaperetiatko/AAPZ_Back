using System;
using System.Collections.Generic;
using AAPZ_Backend.Models;
using AAPZ_Backend.Repositories;

namespace AAPZ_Backend.BusinessLogic.Statistics
{
    public class ClientsWorkplaceStatistic
    {
        WorkplaceOrderRepository workplaceOrderDB;

        public ClientsWorkplaceStatistic()
        {
            workplaceOrderDB = new WorkplaceOrderRepository();
        }

        public Dictionary<int, double> GetStatisticsByYear(int clientId)
        {
            //DateTime finish = DateTime.Now;
            //DateTime start = finish.AddYears(-1);
            DateTime now = DateTime.Now;
            IEnumerable<WorkplaceOrder> workplaceOrders = workplaceOrderDB
                .GetWorkplaceOrdersByClientAndYear(now.Year, clientId);

            Dictionary<int, double> yearStatistics = new Dictionary<int, double>();

            for (int i = 1; i <= 12; i++)
                yearStatistics[i] = 0;

            foreach (WorkplaceOrder workplaceOrder in workplaceOrders)
            {
                double hours = workplaceOrder.FinishTime.Hour - workplaceOrder.StartTime.Hour;
                double minutes = workplaceOrder.FinishTime.Minute - workplaceOrder.StartTime.Minute;
                hours += (minutes / 60);

                yearStatistics[workplaceOrder.FinishTime.Month] += hours;
            }

            for (int i = 1; i <= 12; i++)
            {
                double maximumTime = DateTime.DaysInMonth(now.Year, i) * 12;
                double realTime = yearStatistics[i];
                yearStatistics[i] = Math.Round(realTime / maximumTime, 2);
            }

            return yearStatistics;
        }

        public Dictionary<int, double> GetStatisticsByMonth(int clientId)
        {
           Dictionary<int, double> monthStatistics = new Dictionary<int, double>();
           DateTime now = DateTime.Now;

           IEnumerable<WorkplaceOrder> workplaceOrders = workplaceOrderDB
               .GetWorkplaceOrdersByClientAndMonth(now.Year, now.Month, clientId);

            int dayInMonth = DateTime.DaysInMonth(now.Year, now.Month);

            for (int i = 1; i <= dayInMonth; i++)
                monthStatistics[i] = 0;

            foreach (WorkplaceOrder workplaceOrder in workplaceOrders)
            {
                double hours = workplaceOrder.FinishTime.Hour - workplaceOrder.StartTime.Hour;
                double minutes = workplaceOrder.FinishTime.Minute - workplaceOrder.StartTime.Minute;
                hours += (minutes / 60);

                monthStatistics[workplaceOrder.FinishTime.Day] += hours;
            }


            for (int i = 1; i <= dayInMonth; i++)
            {
                double realTime = monthStatistics[i];
                monthStatistics[i] = Math.Round(realTime / 12, 2);
            }

            return monthStatistics;
        }

        public Dictionary<int, double> GetStatisticsByWeek(int clientId)
        {
            DateTime now = DateTime.Now;
            DayOfWeek dayOfWeek = now.DayOfWeek;

            DateTime start = DateTime.Now;

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(-6);
            }
            else
            {
                start = start.AddDays(-(int)dayOfWeek + 1);
            }
            start = start.AddHours(-now.Hour).AddMinutes(-now.Minute);

            DateTime finish = DateTime.Now;
            finish = start.AddDays(6);
            finish = finish.AddHours(24).AddMinutes(60);

            IEnumerable<WorkplaceOrder> workplaceOrders = workplaceOrderDB
                .GetFilteredWorkplaceOrdersByClient(start, finish, clientId);

            Dictionary<int, double> weekStatistics = new Dictionary<int, double>();

            for (int i = 1; i <= 7; i++)
                weekStatistics[i] = 0;

            foreach (WorkplaceOrder workplaceOrder in workplaceOrders)
            {
                double hours = workplaceOrder.FinishTime.Hour - workplaceOrder.StartTime.Hour;
                double minutes = workplaceOrder.FinishTime.Minute - workplaceOrder.StartTime.Minute;
                hours += (minutes / 60);

                int day;
                if ((int) workplaceOrder.FinishTime.DayOfWeek == 0)
                {
                    day = 7;
                }
                else
                {
                    day = (int) workplaceOrder.FinishTime.DayOfWeek;
                }

                weekStatistics[day] += hours;
            }

            for (int i = 1; i <= 7; i++)
            {
                double realTime = weekStatistics[i];
                weekStatistics[i] = Math.Round(realTime / 12, 2);
            }

            return weekStatistics;
        }
    }
}
