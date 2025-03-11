using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CropDealBackend.Repository
{
    public class SubscriptionRepository : ISubscription
    {
        private readonly CropDealContext _context;

        public SubscriptionRepository(CropDealContext context)
        {
            _context = context;
        }

        //Get All Subscriptions
        public async Task<IEnumerable<Subscription>> GetAllSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        //Get Subscription by ID
        public async Task<Subscription> GetSubscriptionById(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        //Add Subscription
        public async Task<bool> AddSubscription(SubscriptionVM subscription)
        {
            var isExists = await _context.Subscriptions
                .AnyAsync(s => s.DealerId == subscription.DealerId && s.CropId == subscription.CropId);

            if (isExists)
                return false;

            var newSubscription = new Subscription
            {
                DealerId = subscription.DealerId,
                CropId = subscription.CropId,
                SubscriptionDate = subscription.SubscriptionDate,
                IsNotificationEnabled = subscription.IsNotificationEnabled
            };

            _context.Subscriptions.Add(newSubscription);
            await _context.SaveChangesAsync();
            return true;
        }

        //Update Subscription
        public async Task<bool> UpdateSubscription(SubscriptionVM subscription)
        {
            var existingSubscription = await _context.Subscriptions.FindAsync(subscription.Id);

            if (existingSubscription == null)
                return false;

            //Update only fields that matter
            existingSubscription.DealerId = subscription.DealerId;
            existingSubscription.CropId = subscription.CropId;
            existingSubscription.SubscriptionDate = subscription.SubscriptionDate;
            existingSubscription.IsNotificationEnabled = subscription.IsNotificationEnabled;

            await _context.SaveChangesAsync();
            return true;
        }

        //Delete Subscription by ID
        public async Task<bool> DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        //Get Subscription by Dealer ID
        public async Task<Subscription> GetSubscriptionByDealerId(int dealerId)
        {
            return await _context.Subscriptions.Where(s => s.DealerId == dealerId).FirstOrDefaultAsync();
        }

        //Check if Dealer Subscribed to Crop
        public async Task<bool> IsDealerSubscribedToCrop(int dealerId, int cropId)
        {
            return await _context.Subscriptions
                .AnyAsync(s => s.DealerId == dealerId && s.CropId == cropId);
        }

        //Unsubscribe Dealer from Crop
        public async Task<bool> UnsubscribeDealerFromCrop(int dealerId, int cropId)
        {
            var subscription = await _context.Subscriptions
                .Where(s => s.DealerId == dealerId && s.CropId == cropId)
                .FirstOrDefaultAsync();

            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsByDealerId(int dealerId)
        {
            //Fetch all subscriptions where DealerId matches and IsNotificationEnabled is true (or you can use any other active condition)
            return await _context.Subscriptions
                .Where(s => s.DealerId == dealerId)
                .ToListAsync();
        }

    }
}