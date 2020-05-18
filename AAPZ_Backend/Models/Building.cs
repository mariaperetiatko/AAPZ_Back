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
        public double? MonStartTime { get; set; }
        public double? MonFinishTime { get; set; }
        public double? TueStartTime { get; set; }
        public double? TueFinishTime { get; set; }
        public double? WedStartTime { get; set; }
        public double? WedFinishTime { get; set; }
        public double? ThuStartTime { get; set; }
        public double? ThuFinishTime { get; set; }
        public double? FriStartTime { get; set; }
        public double? FriFinishTime { get; set; }
        public double? SatStartTime { get; set; }
        public double? SatFinishTime { get; set; }
        public double? SunStartTime { get; set; }
        public double? SunFinishTime { get; set; }

        public Landlord Landlord { get; set; }
        public ICollection<Workplace> Workplace { get; set; }
    }
}
