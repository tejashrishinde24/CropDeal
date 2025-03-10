using System;
using System.Collections.Generic;

namespace CropDealBackend.Models;

public partial class BankDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string BankAccountNumber { get; set; } = null!;

    public string AccountHolderName { get; set; } = null!;

    public string Ifsccode { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public DateTime AddedAt { get; set; }

    public virtual ICollection<Transaction> TransactionDealerBankAccs { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionFarmerBankAccs { get; set; } = new List<Transaction>();

    public virtual UserDetail User { get; set; } = null!;
}
