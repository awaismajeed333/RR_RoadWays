using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class VoucherDieselDetails
    {
        public int Id { get; set; }
        public int SerialNo { get; set; }
        public DateTime? Date { get; set; }
        public int? StationId { get; set; }
        public int? Litre { get; set; }
        public decimal? Rate { get; set; }
        public decimal? OilAndOthers { get; set; }
        public decimal? Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? VoucherId { get; set; }

        public virtual Station Station { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
