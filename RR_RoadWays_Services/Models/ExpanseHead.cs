using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class ExpanseHead
    {
        public ExpanseHead()
        {
            VoucherOthersExpenses = new HashSet<VoucherOthersExpenses>();
        }

        public int Id { get; set; }
        public string HeadName { get; set; }

        public virtual ICollection<VoucherOthersExpenses> VoucherOthersExpenses { get; set; }
    }
}
