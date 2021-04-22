using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class OverallModel
    {
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class OverallReport
    {
        public decimal? Profit { get; set; }
        public decimal? Claim { get; set; }
        public decimal? Maintenance { get; set; }
        public decimal? FixedExpanse { get; set; }
        public decimal? Installment { get; set; }
        public decimal? TotalRemaining { get; set; }

    }
}
