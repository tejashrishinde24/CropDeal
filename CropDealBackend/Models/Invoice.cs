using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropDealBackend.Models
{

    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int DealerId { get; set; }

        public int FarmerId { get; set; }

        public int CropId { get; set; }

        public string? TransactionStatus { get; set; }

        public decimal PricePerKg { get; set; }

        public decimal? Quantity { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? AddOnId { get; set; }

        public virtual AddOn? AddOn { get; set; }

        public virtual CropDetail Crop { get; set; } = null!;
        [NotMapped]
        public virtual UserDetail Dealer { get; set; } = null!;
        [NotMapped]
        public virtual UserDetail Farmer { get; set; } = null!;
        [NotMapped]
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
