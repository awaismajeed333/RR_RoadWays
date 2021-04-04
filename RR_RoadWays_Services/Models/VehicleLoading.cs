using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class VehicleLoading
    {
        public VehicleLoading()
        {
            VehicleLoadingDetail = new HashSet<VehicleLoadingDetail>();
        }

        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? LoadingDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? VoucherNumber { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleLoadingDetail> VehicleLoadingDetail { get; set; }
    }
}
