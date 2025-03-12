using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropDealBackend.Models
{

    public partial class UserDetail
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Role { get; set; }

        public string? Address { get; set; }

        public string EmailId { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string LoginId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BankDetail> BankDetails { get; set; } = new List<BankDetail>();

        public virtual ICollection<CropDetail> CropDetails { get; set; } = new List<CropDetail>();
        [NotMapped]
        public virtual ICollection<Invoice> InvoiceDealers { get; set; } = new List<Invoice>();
        [NotMapped]
        public virtual ICollection<Invoice> InvoiceFarmers { get; set; } = new List<Invoice>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        [NotMapped]
        public virtual ICollection<Transaction> TransactionDealers { get; set; } = new List<Transaction>();
        [NotMapped]
        public virtual ICollection<Transaction> TransactionFarmers { get; set; } = new List<Transaction>();
    }
}