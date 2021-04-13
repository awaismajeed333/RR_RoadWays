using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Advance
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? StationId { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }

        public virtual Station Station { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
