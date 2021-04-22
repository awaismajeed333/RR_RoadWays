using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class MaintenanceModel
    {
        public string VehicleNumber { get; set; }
        public string DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class MaintenanceReport {
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleNumber { get; set; }
        public string MaintenanceShop { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string Maintenance { get; set; }
        public decimal? Amount { get; set; }
    }
}
