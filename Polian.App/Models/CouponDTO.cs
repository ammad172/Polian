﻿
namespace Polian.App.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public double DiscoutAmount { get; set; }

        public double MinAmount { get; set; }

        public DateTime? LastUpdated { get; set; }

    }
}
