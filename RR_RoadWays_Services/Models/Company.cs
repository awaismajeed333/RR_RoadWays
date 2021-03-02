using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Company
    {
        public Company()
        {
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
