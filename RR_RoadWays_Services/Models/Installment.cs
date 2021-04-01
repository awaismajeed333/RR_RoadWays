using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Installment
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public string InstallmentsMonth { get; set; }
        public DateTime? InstallmentDate { get; set; }
        public string InstallmentDetail { get; set; }
        public int? Amount { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
