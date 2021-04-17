using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class FixedExpenseModel
    {
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class FixedExpenseReport
    {
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleNumber { get; set; }
        public string Month { get; set; }
        public int? StaffSalary { get; set; }
        public int? StaffBhatta { get; set; }
        public int? Amount { get; set; }
    }
}
