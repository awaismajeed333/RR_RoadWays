using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Users
    {
        public Users()
        {
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
