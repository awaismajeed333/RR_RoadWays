using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class FixedExpanse
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public string ExpanseMonth { get; set; }
        public int? StaffSalary { get; set; }
        public int? StaffBhatta { get; set; }
        public string BhattaDetails { get; set; }
        public int? Donation { get; set; }
        public int? DriverRoomRent { get; set; }
        public int? FormanSalary { get; set; }
        public int? ExtraDriversSalary { get; set; }
        public int? ExtraExpense { get; set; }
        public string ExtraExpenseDetails { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
