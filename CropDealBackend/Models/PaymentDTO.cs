namespace CropDealBackend.Models
{
    public class PaymentDto
    {
        //public string PaymentDtoId { get; set; }
        public string? PaymentIntentId { get; set; }

        public int FarmerBankAccId { get; set; }
        public int DealerBankAccId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int InvoiceId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
