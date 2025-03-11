using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    [NotMapped]
    public virtual ICollection<Transaction> TransactionDealerBankAccs { get; set; } = new List<Transaction>();
    [NotMapped]
    public virtual ICollection<Transaction> TransactionFarmerBankAccs { get; set; } = new List<Transaction>();
    [NotMapped]
    public virtual UserDetail User { get; set; } = null!;
}
