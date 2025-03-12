using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropDealBackend.Models
{

    public class Subscription
    {
        public int Id { get; set; }

        public int DealerId { get; set; }

        public int CropId { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public bool IsNotificationEnabled { get; set; }

        public virtual CropDetail Crop { get; set; } = null!;
        [NotMapped]
        public virtual UserDetail Dealer { get; set; } = null!;
    }
}