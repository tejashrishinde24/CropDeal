using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Threading.Tasks;

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly CropDealContext _context;

        public PaymentController(IPaymentService paymentService, CropDealContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        // 1️⃣ Process Payment - Creates PaymentIntent & Returns ClientSecret
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null || paymentDto.Amount <= 0)
            {
                return BadRequest("Invalid payment details.");
            }

            // Process payment with Stripe and get ClientSecret
            var paymentSecret = await _paymentService.ProcessPaymentAsync(paymentDto);

            if (string.IsNullOrEmpty(paymentSecret))
            {
                return BadRequest("Failed to create payment intent.");
            }

            return Ok(new { ClientSecret = paymentSecret });
        }

        [HttpPost("CompletePayment")]
        public async Task<IActionResult> CompletePayment([FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null || string.IsNullOrEmpty(paymentDto.PaymentMethod))
            {
                return BadRequest(new { message = "Invalid payment method." });
            }

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.GetAsync(paymentDto.PaymentIntentId);

            if (paymentIntent.Status == "requires_payment_method")
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    PaymentMethod = paymentDto.PaymentMethod
                };
                await paymentIntentService.UpdateAsync(paymentDto.PaymentIntentId, updateOptions);
            }

            // Fetch the invoice from the database
            var invoice = await _context.Invoices.FindAsync(paymentDto.InvoiceId);
            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found." });
            }

            // Update the TransactionStatus based on payment status
            invoice.TransactionStatus = paymentIntent.Status == "succeeded" ? "Success" : "Pending";

            // Save the updated invoice status to the database
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment updated successfully, and transaction status updated in Invoice table." });
        }



    }
}
