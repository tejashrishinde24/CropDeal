using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropDealBackend.Models
{

    public partial class CropDetail
    {
        public int Id { get; set; }

        public int FarmerId { get; set; }

        public int CropTypeId { get; set; }

        public string CropName { get; set; } = null!;

        public decimal QuantityAvailable { get; set; }

        public string? Status { get; set; }

        public string CropLocation { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual CropType CropType { get; set; } = null!;
        [NotMapped]
        public virtual UserDetail Farmer { get; set; } = null!;
        [NotMapped]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        [NotMapped]
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}