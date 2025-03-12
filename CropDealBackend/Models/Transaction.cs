using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropDealBackend.Models
{

    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int DealerId { get; set; }

        public int FarmerId { get; set; }

        public string TransactionMode { get; set; } = null!;

        public int? FarmerBankAccId { get; set; }

        public int? DealerBankAccId { get; set; }

        public int? InvoiceId { get; set; }
        [NotMapped]
        public virtual UserDetail Dealer { get; set; } = null!;
        [NotMapped]
        public virtual BankDetail? DealerBankAcc { get; set; }
        [NotMapped]
        public virtual UserDetail Farmer { get; set; } = null!;
        [NotMapped]
        public virtual BankDetail? FarmerBankAcc { get; set; }

        public virtual Invoice? Invoice { get; set; }
    }
}