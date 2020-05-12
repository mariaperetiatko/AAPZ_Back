using System;
using System.Collections.Generic;

namespace AAPZ_Backend.Models
{
    public partial class Building
    {
        public Building()
        {
            Workplace = new HashSet<Workplace>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Flat { get; set; }
        public int? LandlordId { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string Name { get; set; }
        public int? StartHour { get; set; }
        public int? StartMinute { get; set; }
        public int? FinistHour { get; set; }
        public int? FinishMinute { get; set; }

        public Landlord Landlord { get; set; }
        public ICollection<Workplace> Workplace { get; set; }
    }
}
