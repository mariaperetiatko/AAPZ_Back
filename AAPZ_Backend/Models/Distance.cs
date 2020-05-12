using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAPZ_Backend.Models
{
    public class Distance
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double DistanceValue { get; set; }
        public DateTime Date { get; set; }

        public Client Client { get; set; }
    }

}
