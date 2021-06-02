using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class DataValidation
    {
        public int Id { get; set; }
        public int Vehicle { get; set; }
        public DateTime? Srp { get; set; }
        public DateTime? Prp { get; set; }
        public DateTime? KpkRp { get; set; }
        public DateTime? TaxPaper { get; set; }
        public DateTime? Fitness { get; set; }
        public DateTime? Insurance { get; set; }

        public virtual Vehicle VehicleNavigation { get; set; }
    }
}
