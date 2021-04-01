using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class VehicleClaim
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string Claim { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
