using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAPZ_Backend.Models
{
    public class WorkplaceParameter
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EquipmentId { get; set; }
        public int Count { get; set; }
        public int Priority { get; set; }
        public Client Client { get; set; }
        public Equipment Equipment { get; set; }
    }
}
