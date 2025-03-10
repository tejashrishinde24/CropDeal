using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IInvoice

    {

        // 🔹 Get all invoices

        Task<IEnumerable<Invoice>> GetAllInvoices();

        // 🔹 Get invoice by ID

        Task<Invoice> GetInvoiceById(int invoiceId);


        // 🔹 Get invoices by dealer ID

        Task<IEnumerable<Invoice>> GetInvoicesByDealer(int dealerId);

        // 🔹 Get invoices by farmer ID

        Task<IEnumerable<Invoice>> GetInvoicesByFarmer(int farmerId);

        // 🔹 Get invoices within a date range

        Task<IEnumerable<Invoice>> GetInvoicesBetweenDates(DateTime startDate, DateTime endDate);

        // 🔹 Get invoices that are still unpaid

        Task<IEnumerable<Invoice>> GetPendingInvoices();

        // 🔹 Search invoices by farmer or dealer name

        //Task<IEnumerable<Invoice>> SearchInvoicesByName(string name);

        // 🔹 Mark an invoice as paid

        Task<bool> MarkInvoiceAsPaid(int invoiceId);

        // 🔹 Apply a discount before payment

        Task<Invoice> ApplyDiscount(int invoiceId, decimal discountPercentage);

        // 🔹 Duplicate an invoice (for repeat transactions)

        Task<Invoice> DuplicateInvoice(int invoiceId);

        // 🔹 Send an invoice copy to an email

        Task<bool> SendInvoiceToEmail(int invoiceId, string email);

        // 🔹 Validate bank account before payment

        Task<bool> ValidateBankAccount(int dealerId);

        // 🔹 Auto-calculate total amount (based on price per kg and quantity)

        //Task<decimal> CalculateTotalAmount(decimal pricePerKg, decimal quantity);

        // 🔹 Add late payment fee to an invoice

        //Task<Invoice?> AddLatePaymentFee(int invoiceId, decimal lateFee);

        // 🔹 Generate report for a specific farmer

        Task<IEnumerable<Invoice>> GenerateFarmerReport(int farmerId);

    }

}
