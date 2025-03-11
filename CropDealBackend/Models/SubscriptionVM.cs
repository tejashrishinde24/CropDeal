namespace CropDealBackend.Models
{
    public class SubscriptionVM
    {
        public int Id { get; set; }

        public int DealerId { get; set; }

        public int CropId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public bool IsNotificationEnabled { get; set; }
    }
}
