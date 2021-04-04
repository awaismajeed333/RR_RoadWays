using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class VehicleLoadingDetail
    {
        public int Id { get; set; }
        public int VloadingId { get; set; }
        public string VehicleName { get; set; }
        public string Description { get; set; }

        public virtual VehicleLoading Vloading { get; set; }
    }
}
