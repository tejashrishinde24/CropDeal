namespace CropDealBackend.Models
{
    public class InvoiceVM
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
    }
}
