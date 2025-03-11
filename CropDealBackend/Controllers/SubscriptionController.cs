using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscription _subscriptionRepository;

        public SubscriptionController(ISubscription subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        // Get All Subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetAllSubscriptions()
        {
            var subscriptions = await _subscriptionRepository.GetAllSubscriptions();
            return Ok(subscriptions);
        }

        //Get Subscription by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscriptionById(int id)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionById(id);
            if (subscription == null)
                return NotFound("Subscription not found");

            return Ok(subscription);
        }

        //Add Subscription
        [HttpPost]
        public async Task<ActionResult> AddSubscription([FromBody] SubscriptionVM subscription)
        {
            bool result = await _subscriptionRepository.AddSubscription(subscription);
            if (!result)
                return Conflict("Subscription already exists");

            return Ok("Subscription added successfully");
        }

        //Update Subscription
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubscription(int id, [FromBody] SubscriptionVM subscription)
        {
            if (id != subscription.Id)
                return BadRequest("ID Mismatch");

            bool result = await _subscriptionRepository.UpdateSubscription(subscription);
            if (!result)
                return NotFound("Subscription not found");

            return Ok("Subscription updated successfully");
        }

        //Delete Subscription
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            bool result = await _subscriptionRepository.DeleteSubscription(id);
            if (!result)
                return NotFound("Subscription not found");

            return Ok("Subscription deleted successfully");
        }

        //Check if Dealer Subscribed to Crop
        [HttpGet("dealer/{dealerId}/crop/{cropId}")]
        public async Task<ActionResult<bool>> IsDealerSubscribedToCrop(int dealerId, int cropId)
        {
            bool isSubscribed = await _subscriptionRepository.IsDealerSubscribedToCrop(dealerId, cropId);
            return Ok(isSubscribed);
        }

        //Unsubscribe Dealer from Crop
        [HttpDelete("dealer/{dealerId}/crop/{cropId}")]
        public async Task<ActionResult> UnsubscribeDealerFromCrop(int dealerId, int cropId)
        {
            bool result = await _subscriptionRepository.UnsubscribeDealerFromCrop(dealerId, cropId);
            if (!result)
                return NotFound("Subscription not found");

            return Ok("Unsubscribed successfully");
        }

        [HttpGet("dealer/{dealerId}/active")]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetActiveSubscriptionsByDealerId(int dealerId)
        {
            var activeSubscriptions = await _subscriptionRepository.GetActiveSubscriptionsByDealerId(dealerId);

            if (!activeSubscriptions.Any())
            {
                return NotFound("No active subscriptions found for this dealer.");
            }

            return Ok(activeSubscriptions);
        }
        [HttpGet("dealer/{dealerId}")]
        public async Task<ActionResult<Subscription>> GetSubscriptionByDealerId(int dealerId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionByDealerId(dealerId);
            if (subscription == null)
                return NotFound($"No subscription found for Dealer with ID: {dealerId}");
            return Ok(subscription);
        }

    }
}