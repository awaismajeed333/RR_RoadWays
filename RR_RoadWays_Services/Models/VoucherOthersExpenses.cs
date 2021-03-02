﻿using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class VoucherOthersExpenses
    {
        public int Id { get; set; }
        public int SerialNo { get; set; }
        public string OthersExpense { get; set; }
        public decimal? Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? VoucherId { get; set; }

        public virtual Voucher Voucher { get; set; }
    }
}
