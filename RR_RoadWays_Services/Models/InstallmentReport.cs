using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class InstallmentModel
    {
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class InstallmentReport
    {
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleNumber { get; set; }
        public string InstallmentMonth { get; set; }
        public string InstallmentDetails { get; set; }
        public int? Amount { get; set; }
    }
}
