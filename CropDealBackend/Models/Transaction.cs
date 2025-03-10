using System;
using System.Collections.Generic;

namespace CropDealBackend.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int DealerId { get; set; }

    public int FarmerId { get; set; }

    public string TransactionMode { get; set; } = null!;

    public int? FarmerBankAccId { get; set; }

    public int? DealerBankAccId { get; set; }

    public int? InvoiceId { get; set; }

    public virtual UserDetail Dealer { get; set; } = null!;

    public virtual BankDetail? DealerBankAcc { get; set; }

    public virtual UserDetail Farmer { get; set; } = null!;

    public virtual BankDetail? FarmerBankAcc { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
