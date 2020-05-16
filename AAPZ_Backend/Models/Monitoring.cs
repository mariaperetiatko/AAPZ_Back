using System;

namespace AAPZ_Backend.Models
{
    public class Monitoring
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public double RightValues { get; set; }

        public Client Client { get; set; }
    }
}
