using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RR_RoadWays_Services.Models
{
    public class PumpModel
    {
        public string PumpId { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PumpReport
    {
        public int SerialNo { get; set; }
        public string VoucherNumber { get; set; }
        public string Vehicle { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Litre { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string OilAndOther { get; set; }
        public decimal? OilAmount { get; set; }
        public decimal? Total { get; set; }
    }
}
