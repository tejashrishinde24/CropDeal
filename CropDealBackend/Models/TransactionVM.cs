namespace CropDealBackend.Models
{
    public class TransactionVM
    {
        public int TransactionId { get; set; }

        public int BankAccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int DealerId { get; set; }

        public int FarmerId { get; set; }

        public string TransactionMode { get; set; } = null!;

        public int? FarmerBankAccId { get; set; }

        public int? DealerBankAccId { get; set; }

        public int? InvoiceId { get; set; }

    }
}
