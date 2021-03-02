using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class City
    {
        public City()
        {
            Station = new HashSet<Station>();
            VoucherDownFromNavigation = new HashSet<Voucher>();
            VoucherDownToNavigation = new HashSet<Voucher>();
            VoucherUpToNavigation = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Station> Station { get; set; }
        public virtual ICollection<Voucher> VoucherDownFromNavigation { get; set; }
        public virtual ICollection<Voucher> VoucherDownToNavigation { get; set; }
        public virtual ICollection<Voucher> VoucherUpToNavigation { get; set; }
    }
}
