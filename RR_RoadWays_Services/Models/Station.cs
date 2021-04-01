using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Station
    {
        public Station()
        {
            Maintenance = new HashSet<Maintenance>();
            Voucher = new HashSet<Voucher>();
            VoucherDieselDetails = new HashSet<VoucherDieselDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string OwnerName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string StationType { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Maintenance> Maintenance { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
        public virtual ICollection<VoucherDieselDetails> VoucherDieselDetails { get; set; }
    }
}
