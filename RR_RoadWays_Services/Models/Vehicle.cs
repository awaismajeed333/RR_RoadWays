using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Advance = new HashSet<Advance>();
            DataValidation = new HashSet<DataValidation>();
            FixedExpanse = new HashSet<FixedExpanse>();
            Installment = new HashSet<Installment>();
            Maintenance = new HashSet<Maintenance>();
            VehicleClaim = new HashSet<VehicleClaim>();
            VehicleLoading = new HashSet<VehicleLoading>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Advance> Advance { get; set; }
        public virtual ICollection<DataValidation> DataValidation { get; set; }
        public virtual ICollection<FixedExpanse> FixedExpanse { get; set; }
        public virtual ICollection<Installment> Installment { get; set; }
        public virtual ICollection<Maintenance> Maintenance { get; set; }
        public virtual ICollection<VehicleClaim> VehicleClaim { get; set; }
        public virtual ICollection<VehicleLoading> VehicleLoading { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
