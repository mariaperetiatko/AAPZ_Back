using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAPZ_Backend.BusinessLogic.Classes
{
    public class Scheduler
    {
        public string Id { get; set; }
        public string Start_date { get; set; }
        public string End_date { get; set; }
        public string Text { get; set; }
        public string Details { get; set; }

        public Scheduler(string id, string start_date, string end_date, string text, string details)
        {
            Id = id;
            Start_date = start_date;
            End_date = end_date;
            Text = text;
            Details = details;
        }

    }
}
