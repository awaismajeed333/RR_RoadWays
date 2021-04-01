using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Maintenance
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public int? StationId { get; set; }
        public int? DepartmentId { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual Station Station { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
