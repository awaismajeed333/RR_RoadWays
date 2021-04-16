using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class ClaimModel
    {
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ClaimReport
    {
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleNumber { get; set; }
        public string Claim { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
    }
}
