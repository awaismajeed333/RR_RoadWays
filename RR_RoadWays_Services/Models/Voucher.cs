using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            VoucherDieselDetails = new HashSet<VoucherDieselDetails>();
            VoucherOthersExpenses = new HashSet<VoucherOthersExpenses>();
        }

        public int Id { get; set; }
        public int? VehicleNumber { get; set; }
        public string Month { get; set; }
        public DateTime? UpDate { get; set; }
        public int? UpFrom { get; set; }
        public int? UpTo { get; set; }
        public decimal? UpAmount { get; set; }
        public DateTime? DownReturnDate { get; set; }
        public int? DownFrom { get; set; }
        public int? DownTo { get; set; }
        public string DownDescription { get; set; }
        public decimal? DownAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedById { get; set; }
        public int? OilShopId { get; set; }
        public decimal? OilAmount { get; set; }
        public string UpDescription { get; set; }
        public string VoucherNumber { get; set; }

        public virtual Users CreatedBy { get; set; }
        public virtual City DownFromNavigation { get; set; }
        public virtual City DownToNavigation { get; set; }
        public virtual Station OilShop { get; set; }
        public virtual Company UpFromNavigation { get; set; }
        public virtual City UpToNavigation { get; set; }
        public virtual Vehicle VehicleNumberNavigation { get; set; }
        public virtual ICollection<VoucherDieselDetails> VoucherDieselDetails { get; set; }
        public virtual ICollection<VoucherOthersExpenses> VoucherOthersExpenses { get; set; }
    }
}
