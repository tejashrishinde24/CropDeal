namespace CropDealBackend.Models
{
    public class AddOnVM
    {
        public int AddOnId { get; set; }

        public int AdminId { get; set; }

        public int AddOnTypeId { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string? AddOnName { get; set; }

    }
}
