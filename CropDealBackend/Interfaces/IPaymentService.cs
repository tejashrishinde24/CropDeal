using System.Threading.Tasks;
using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(PaymentDto paymentDto);  // Create PaymentIntent
        Task<string> CompletePaymentAsync(PaymentDto paymentDto); // Verify PaymentIntent status
    }
}
