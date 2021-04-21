using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class CashAdvanceModel
    {
        public string PickupPointId { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class CashAdvanceReport
    {
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleNumber { get; set; }
        public string PickupPoint { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
    }
}
