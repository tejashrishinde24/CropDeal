using System;
using System.Collections.Generic;

namespace CropDealBackend.Models
{

    public partial class Notification
    {
        public int Id { get; set; }

        public int DealerId { get; set; }

        public int CropId { get; set; }

        public string Message { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public bool? IsRead { get; set; }

        public virtual CropDetail Crop { get; set; } = null!;

        public virtual UserDetail Dealer { get; set; } = null!;
    }
}