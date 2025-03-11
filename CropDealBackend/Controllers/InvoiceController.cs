//controller
using CropDealBackend.Interfaces;
using CropDealBackend.Models;
//using CropDealBackend.NewFolder;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoice _invoiceRepository;

        public InvoiceController(IInvoice invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        // ✅ Create a new invoice
        [HttpPost("CreateInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceVM invoice)
        {
            var createdInvoice = await _invoiceRepository.CreateInvoice(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvoiceId }, createdInvoice);
        }

        // ✅ Update an existing invoice
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceVM invoice)
        {
            if (id != invoice.InvoiceId)
                return BadRequest("Invoice ID mismatch");

            var updatedInvoice = await _invoiceRepository.UpdateInvoice(invoice);
            return Ok(updatedInvoice);
        }

        // ✅ Delete an invoice by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            bool result = await _invoiceRepository.DeleteInvoice(id);
            if (!result)
                return NotFound("Invoice not found");

            return Ok("Invoice deleted successfully");
        }


        // ✅ Get all invoices
        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoices();
            return Ok(invoices);
        }

        // ✅ Get invoice by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
                return NotFound("Invoice not found");
            return Ok(invoice);
        }

        // ✅ Get invoices by dealer ID
        [HttpGet("dealer/{dealerId}")]
        public async Task<IActionResult> GetInvoicesByDealer(int dealerId)
        {
            var invoices = await _invoiceRepository.GetInvoicesByDealer(dealerId);
            return Ok(invoices);
        }

        // ✅ Get invoices by farmer ID
        [HttpGet("farmer/{farmerId}")]
        public async Task<IActionResult> GetInvoicesByFarmer(int farmerId)
        {
            var invoices = await _invoiceRepository.GetInvoicesByFarmer(farmerId);
            return Ok(invoices);
        }

        // ✅ Get invoices within a date range
        [HttpGet("between-dates")]
        public async Task<IActionResult> GetInvoicesBetweenDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var invoices = await _invoiceRepository.GetInvoicesBetweenDates(startDate, endDate);
            return Ok(invoices);
        }

        // ✅ Get pending invoices
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingInvoices()
        {
            var invoices = await _invoiceRepository.GetPendingInvoices();
            return Ok(invoices);
        }


        // ✅ Mark invoice as paid
        [HttpPatch("mark-paid/{invoiceId}")]
        public async Task<IActionResult> MarkInvoiceAsPaid(int invoiceId)
        {
            bool result = await _invoiceRepository.MarkInvoiceAsPaid(invoiceId);
            if (!result)
                return BadRequest("Invoice could not be marked as paid");
            return Ok("Invoice marked as paid successfully");
        }

        // ✅ Apply discount to an invoice
        [HttpPut("apply-discount/{invoiceId}")]
        public async Task<IActionResult> ApplyDiscount(int invoiceId, [FromQuery] decimal discountPercentage)
        {
            var invoice = await _invoiceRepository.ApplyDiscount(invoiceId, discountPercentage);
            if (invoice == null)
                return NotFound("Invoice not found");
            return Ok(invoice);
        }
        // ✅ Duplicate an invoice
        [HttpPost("duplicate/{invoiceId}")]
        public async Task<IActionResult> DuplicateInvoice(int invoiceId)
        {
            var newInvoice = await _invoiceRepository.DuplicateInvoice(invoiceId);
            if (newInvoice == null)
                return NotFound("Original invoice not found");
            return CreatedAtAction(nameof(GetInvoiceById), new { id = newInvoice.InvoiceId }, newInvoice);
        }
        // ✅ Send invoice via email
        [HttpPost("send-email/{invoiceId}")]
        public async Task<IActionResult> SendInvoiceToEmail(int invoiceId, [FromQuery] string email)
        {
            bool result = await _invoiceRepository.SendInvoiceToEmail(invoiceId, email);
            if (!result)
                return BadRequest("Failed to send invoice via email");
            return Ok("Invoice sent successfully");
        }

        // ✅ Validate dealer's bank account
        [HttpGet("validate-bank/{dealerId}")]
        public async Task<IActionResult> ValidateBankAccount(int dealerId)
        {
            bool isValid = await _invoiceRepository.ValidateBankAccount(dealerId);
            if (!isValid)
                return BadRequest("Invalid bank account details");
            return Ok("Bank account is valid");
        }
        // ✅ Generate farmer report
        [HttpGet("farmer-report/{farmerId}")]
        public async Task<IActionResult> GenerateFarmerReport(int farmerId)
        {
            var invoices = await _invoiceRepository.GenerateFarmerReport(farmerId);
            return Ok(invoices);
        }




    }
}