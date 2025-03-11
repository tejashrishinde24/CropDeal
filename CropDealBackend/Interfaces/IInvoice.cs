//interface
using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IInvoice
    {

        // 🔹 Create a new invoice
        Task<Invoice> CreateInvoice(InvoiceVM invoice);

        // 🔹 Update an existing invoice
        Task<Invoice> UpdateInvoice(InvoiceVM invoice);

        // 🔹 Delete an invoice by ID
        Task<bool> DeleteInvoice(int invoiceId);

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
        // 🔹 Generate report for a specific farmer 
        Task<IEnumerable<Invoice>> GenerateFarmerReport(int farmerId);



    }
}
