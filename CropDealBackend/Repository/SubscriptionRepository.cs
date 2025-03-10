using CropDealBackend.Models;

namespace CropDealBackend.Repository
{
    public class SubscriptionRepository
    {
        private CropDealContext _context;
        public SubscriptionRepository(CropDealContext context)
        {
            _context = context;
        }
    }
}
