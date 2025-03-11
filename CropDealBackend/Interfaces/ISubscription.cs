using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface ISubscription
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptions();
        Task<Subscription> GetSubscriptionById(int id);
        Task<bool> AddSubscription(SubscriptionVM subscription);
        Task<bool> UpdateSubscription(SubscriptionVM subscription);
        Task<bool> DeleteSubscription(int id);
        Task<Subscription> GetSubscriptionByDealerId(int dealerId);
        Task<bool> IsDealerSubscribedToCrop(int dealerId, int cropId);
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsByDealerId(int dealerId);
        Task<bool> UnsubscribeDealerFromCrop(int dealerId, int cropId);
    }
}