using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CropDealBackend.Repository
{
    public class InvoiceRepository:IInvoice
    {

            private readonly CropDealContext _context;

            public InvoiceRepository(CropDealContext context)

            {

                _context = context;

            }



            // ✅ Get all invoices

            public async Task<IEnumerable<Invoice>> GetAllInvoices()

            {

                return _context.Invoices.ToList();

            }

            // ✅ Get invoice by ID

            public async Task<Invoice?> GetInvoiceById(int invoiceId)

            {

                return await _context.Invoices.FindAsync(invoiceId);

            }

            // ✅ Get invoices by dealer ID

            public async Task<IEnumerable<Invoice>> GetInvoicesByDealer(int dealerId)

            {

            return await _context.Invoices.Where(i => i.DealerId == dealerId).ToListAsync();

            }

            // ✅ Get invoices by farmer ID

            public async Task<IEnumerable<Invoice>> GetInvoicesByFarmer(int farmerId)

            {

                return await _context.Invoices

                                     .Where(i => i.FarmerId == farmerId)

                                     .ToListAsync();

            }

            // ✅ Get invoices between two dates

            public async Task<IEnumerable<Invoice>> GetInvoicesBetweenDates(DateTime startDate, DateTime endDate)

            {

                //return await _context.Invoices

                //                     .Where(i => i.PurchaseDate >= startDate && i.PurchaseDate <= endDate)

                //                     .ToListAsync();

                return await _context.Invoices

                                 .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate)

                                 .ToListAsync();

            }

            // ✅ Get pending invoices (not paid)

            public async Task<IEnumerable<Invoice>> GetPendingInvoices()

            {

                return await _context.Invoices

                                     .Where(i => i.TransactionStatus != "Completed")

                                     .ToListAsync();

            }



            // ✅ Mark invoice as paid

            public async Task<bool> MarkInvoiceAsPaid(int invoiceId)

            {

                var invoice = await _context.Invoices.FindAsync(invoiceId);

                if (invoice == null) return false;

                invoice.TransactionStatus = "Completed";

                await _context.SaveChangesAsync();

                return true;

            }

            // ✅ Apply discount to an invoice

            public async Task<Invoice?> ApplyDiscount(int invoiceId, decimal discountPercentage)

            {

                var invoice = await _context.Invoices.FindAsync(invoiceId);

                if (invoice == null) return null;

                decimal discountAmount = (decimal)((invoice.TotalAmount * discountPercentage) / 100);

                invoice.TotalAmount -= discountAmount;

                await _context.SaveChangesAsync();

                return invoice;

            }

            public async Task<Invoice> DuplicateInvoice(int invoiceId)

            {

                //throw new NotImplementedException();

                //var invoice = await _context.Invoices.FindAsync(invoiceId);

                //if (invoice == null) throw new ArgumentException("Invoice not found");

                //var duplicatedInvoice = new Invoice

                //{

                //    DealerId = invoice.DealerId,

                //    FarmerId = invoice.FarmerId,

                //    PurchaseDate = DateTime.Now, // Assuming the duplicated invoice has the current date

                //    TotalAmount = invoice.TotalAmount,

                //    TransactionStatus = "Pending" // Assuming the duplicated invoice starts as pending

                //};

                //_context.Invoices.Add(duplicatedInvoice);

                //await _context.SaveChangesAsync();

                //return duplicatedInvoice;

                var invoice = await _context.Invoices.FindAsync(invoiceId);

                if (invoice == null) throw new ArgumentException("Invoice not found");

                // Check if the CropId exists in the CropDetails table

                var cropExists = await _context.CropDetails.AnyAsync(c => c.Id == invoice.CropId);

                if (!cropExists) throw new ArgumentException("Invalid CropId. The specified CropId does not exist.");

                var duplicatedInvoice = new Invoice

                {

                    DealerId = invoice.DealerId,

                    FarmerId = invoice.FarmerId,

                    CropId = invoice.CropId, // Ensure CropId is duplicated

                    PurchaseDate = DateTime.Now, // Assuming the duplicated invoice has the current date

                    TotalAmount = invoice.TotalAmount,

                    TransactionStatus = "Pending" // Assuming the duplicated invoice starts as pending

                };

                _context.Invoices.Add(duplicatedInvoice);

                await _context.SaveChangesAsync();

                return duplicatedInvoice;

            }

            // ✅ Send an invoice to email (Placeholder for now)

            public async Task<bool> SendInvoiceToEmail(int invoiceId, string email)

            {

                var invoice = await _context.Invoices.FindAsync(invoiceId);

                if (invoice == null) return false;

                // Simulating email sending (Integration with an email service like SendGrid can be added)

                Console.WriteLine($"Invoice {invoiceId} sent to {email}.");

                return true;

            }

            // ✅ Validate bank account before payment (Placeholder logic)

            public async Task<bool> ValidateBankAccount(int dealerId)

            {

                var dealer = await _context.BankDetails.FindAsync(dealerId);

                return dealer != null && !string.IsNullOrEmpty(dealer.BankAccountNumber);

            }



            public async Task<IEnumerable<Invoice>> GenerateFarmerReport(int farmerId)

            {

                //throw new NotImplementedException();

                return await _context.Invoices

                                .Where(i => i.FarmerId == farmerId)

                                .ToListAsync();

            }

            public async Task<Invoice> CreateInvoice(InvoiceVM invoiceObj)

            {

                //throw new NotImplementedException();

                Invoice invoice = new Invoice()
                {

                    InvoiceId = invoiceObj.InvoiceId,

                    DealerId = invoiceObj.DealerId,

                    FarmerId = invoiceObj.FarmerId,

                    CropId = invoiceObj.CropId,

                    PricePerKg = invoiceObj.PricePerKg,

                    Quantity = invoiceObj.Quantity,

                    TotalAmount = invoiceObj.TotalAmount,

                    TransactionStatus = invoiceObj.TransactionStatus,

                    //PurchaseDate = invoiceObj.PurchaseDate,

                    //InvoiceDate = invoiceObj.InvoiceDate,

                    AddOnId = invoiceObj.AddOnId


                };

                _context.Invoices.Add(invoice);

                await _context.SaveChangesAsync();

                return invoice;

            }

            public async Task<Invoice> UpdateInvoice(InvoiceVM invoiceVM)

            {

                //throw new NotImplementedException();

                //_context.Invoices.Update(invoice);

                //await _context.SaveChangesAsync();

                //return invoice;

                var existingInvoice = await _context.Invoices.FindAsync(invoiceVM.InvoiceId);

                if (existingInvoice == null)

                {

                    throw new ArgumentException("Invoice not found");

                }

                // Update the properties of the existing invoice

                existingInvoice.DealerId = invoiceVM.DealerId;

                existingInvoice.FarmerId = invoiceVM.FarmerId;

                existingInvoice.CropId = invoiceVM.CropId;

                existingInvoice.PricePerKg = invoiceVM.PricePerKg;

                existingInvoice.Quantity = invoiceVM.Quantity;

                existingInvoice.TotalAmount = invoiceVM.TotalAmount;

                existingInvoice.TransactionStatus = invoiceVM.TransactionStatus;

                existingInvoice.PurchaseDate = invoiceVM.PurchaseDate;

                existingInvoice.InvoiceDate = invoiceVM.InvoiceDate;

                existingInvoice.AddOnId = invoiceVM.AddOnId;

                // Save the changes to the database

                await _context.SaveChangesAsync();

                return existingInvoice;

            }

            public async Task<bool> DeleteInvoice(int invoiceId)

            {

                //throw new NotImplementedException();

                var invoice = await _context.Invoices.FindAsync(invoiceId);

                if (invoice == null) return false;

                _context.Invoices.Remove(invoice);

                await _context.SaveChangesAsync();

                return true;

            }



        }

    }
