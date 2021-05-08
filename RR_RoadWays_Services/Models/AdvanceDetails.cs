using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class AdvanceDetails
    {
        public int Id { get; set; }
        public int? AdvanceId { get; set; }
        public int? StationId { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public virtual Advance Advance { get; set; }
        public virtual Station Station { get; set; }
    }
}
