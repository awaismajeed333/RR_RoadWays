using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Advance
    {
        public Advance()
        {
            AdvanceDetails = new HashSet<AdvanceDetails>();
        }

        public int Id { get; set; }
        public int? VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<AdvanceDetails> AdvanceDetails { get; set; }
    }
}
