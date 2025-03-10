namespace CropDealBackend.Models
{
    public class InvoiceVM
    {
        public int InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int DealerId { get; set; }

        public int FarmerId { get; set; }

        public int CropId { get; set; }

        public string? TransactionStatus { get; set; }

        public decimal PricePerKg { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
