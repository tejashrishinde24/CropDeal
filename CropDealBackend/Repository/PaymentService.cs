using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Threading.Tasks;

namespace CropDealBackend.Repository
{
    public class PaymentService : IPaymentService
    {
        private readonly CropDealContext _context;

        public PaymentService(CropDealContext context)
        {
            _context = context;
        }

        // 1️⃣ Create PaymentIntent & Return ClientSecret
        public async Task<string> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51R1jimIOzzE1An9ux6cPbrQQd2dwlm350CVKPFB3LWHx0tJplPL56bjKTU97oTcw6W2yOTo4RyWdG4rYmcMOkeN400n1aLOU34"; // 🔴 Set your Stripe secret key

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(paymentDto.Amount * 100), // Convert amount to cents
                    Currency = paymentDto.Currency,
                    PaymentMethodTypes = new List<string> { "card" }, // ✅ Ensure this is set
                    PaymentMethod = paymentDto.PaymentMethod, // ✅ Attach payment method
                    Confirm = true // ✅ Auto-confirm the payment if possible
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return paymentIntent.ClientSecret; // ✅ Return ClientSecret for frontend
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating payment: {ex.Message}");
                return string.Empty;
            }
        }

        // 2️⃣ Verify PaymentIntent Status
        public async Task<string> CompletePaymentAsync(PaymentDto paymentDto)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51R1jimIOzzE1An9ux6cPbrQQd2dwlm350CVKPFB3LWHx0tJplPL56bjKTU97oTcw6W2yOTo4RyWdG4rYmcMOkeN400n1aLOU34"; // 🔴 Set your Stripe secret key

                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentDto.PaymentIntentId);

                if (paymentIntent.Status == "requires_payment_method")
                {
                    // ✅ Attach a valid payment method and confirm the payment
                    var updateOptions = new PaymentIntentUpdateOptions
                    {
                        PaymentMethod = paymentDto.PaymentMethod
                    };
                    await service.UpdateAsync(paymentDto.PaymentIntentId, updateOptions);

                    var confirmOptions = new PaymentIntentConfirmOptions();
                    paymentIntent = await service.ConfirmAsync(paymentDto.PaymentIntentId, confirmOptions);
                }

                Console.WriteLine($"Stripe PaymentIntent Status: {paymentIntent.Status}");

                if (paymentIntent.Status == "succeeded")
                {
                    return "success";
                }
                else
                {
                    return $"failed - {paymentIntent.Status}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying payment: {ex.Message}");
                return "failed";
            }
        }
    }
}
