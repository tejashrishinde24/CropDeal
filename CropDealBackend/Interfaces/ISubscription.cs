using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface ISubscription
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptions();
        Task<Subscription> GetSubscriptionById(int id);
        Task<bool> AddSubscription(Subscription subscription);
        Task<bool> UpdateSubscription(Subscription subscription);
        Task<bool> DeleteSubscription(int id);
        Task<Subscription> GetSubscriptionByIDealerId(int dealerId);
        Task<bool> IsDealerSubscribedToCrop(int dealerId, int cropId);
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsByDealerId(int dealerId);
        Task<bool> UnsubscribeDealerFromCrop(int dealerId, int cropId);
    }
}
