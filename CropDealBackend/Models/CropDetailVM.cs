namespace CropDealBackend.Models
{
    public class CropDetailVM
    {
        public int Id { get; set; }

        public int FarmerId { get; set; }

        public int CropTypeId { get; set; }

        public string CropName { get; set; } = null!;

        public decimal QuantityAvailable { get; set; }

        public string? Status { get; set; }

        public string CropLocation { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

    }
}
