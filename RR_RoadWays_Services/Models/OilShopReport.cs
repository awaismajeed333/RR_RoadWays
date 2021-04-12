using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class OilShopModel
    {
        public string OilShopId { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class OilShopReport {
        public int SerialNo { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
    }
}
