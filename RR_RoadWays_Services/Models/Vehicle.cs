using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
