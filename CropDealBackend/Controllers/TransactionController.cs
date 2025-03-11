using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{

    public class TransactionController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class TransactionsController : ControllerBase
        {
            //Adding Dependancey Injection

            private readonly ITransactions _transactionRepository;
            public TransactionsController(ITransactions transactionsRepository)
            {
                _transactionRepository = transactionsRepository;
            }

            // GET: api/Transaction
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
            {
                var transactions = await _transactionRepository.GetAllTransactions();
                return Ok(transactions);
            }

            // GET: api/Transaction/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<Transaction>> GetTransactionById(int id)
            {
                var transaction = await _transactionRepository.GetTransactionById(id);

                if (transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);
            }

            // GET: api/Transaction/dealer/{dealerId}
            [HttpGet("dealer/{dealerId}")]
            public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDealerId(int dealerId)
            {
                var transactions = await _transactionRepository.GetTransactionsByDealerId(dealerId);
                return Ok(transactions);
            }

            // GET: api/Transaction/farmer/{farmerId}
            [HttpGet("farmer/{farmerId}")]
            public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByFarmerId(int farmerId)
            {
                var transactions = await _transactionRepository.GetTransactionsByFarmerId(farmerId);
                return Ok(transactions);
            }

            // GET: api/Transaction/dateRange?startDate=2025-03-01&endDate=2025-03-08
            [HttpGet("dateRange")]
            public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
            {
                var transactions = await _transactionRepository.GetTransactionsByDateRange(startDate, endDate);
                return Ok(transactions);
            }

            // GET: api/Transaction/mode/{transactionMode}
            [HttpGet("mode/{transactionMode}")]
            public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByMode(string transactionMode)
            {
                var transactions = await _transactionRepository.GetTransactionsByMode(transactionMode);
                return Ok(transactions);
            }

            // POST: api/Transaction
            [HttpPost]
            public async Task<ActionResult> CreateTransaction(TransactionVM transaction)
            {
                var success = await _transactionRepository.CreateTransaction(transaction);
                if (!success)
                {
                    return BadRequest("Failed to create the transaction.");
                }

                return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
            }

            // PUT: api/Transaction/{id}
            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateTransaction(int id, TransactionVM transaction)
            {
                if (id != transaction.TransactionId)
                {
                    return BadRequest("Transaction ID mismatch.");
                }

                var success = await _transactionRepository.UpdateTransaction(transaction);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/Transaction/{id}
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteTransaction(int id)
            {
                var success = await _transactionRepository.DeleteTransaction(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // GET: api/Transaction/totalAmount
            [HttpGet("totalAmount")]
            public async Task<ActionResult<decimal>> GetTotalTransactionAmount()
            {
                var totalAmount = await _transactionRepository.GetTotalTransactionAmount();
                return Ok(totalAmount);
            }

            // GET: api/Transaction/dealer/{dealerId}/totalAmount
            [HttpGet("dealer/{dealerId}/totalAmount")]
            public async Task<ActionResult<decimal>> GetTotalTransactionAmountForDealer(int dealerId)
            {
                var totalAmount = await _transactionRepository.GetTotalTransactionAmountForDealer(dealerId);
                return Ok(totalAmount);
            }

            // GET: api/Transaction/farmer/{farmerId}/totalAmount
            [HttpGet("farmer/{farmerId}/totalAmount")]
            public async Task<ActionResult<decimal>> GetTotalTransactionAmountForFarmer(int farmerId)
            {
                var totalAmount = await _transactionRepository.GetTotalTransactionAmountForFarmer(farmerId);
                return Ok(totalAmount);
            }

            // GET: api/Transaction/exists/{id}
            [HttpGet("exists/{id}")]
            public async Task<ActionResult<bool>> TransactionExists(int id)
            {
                var exists = await _transactionRepository.TransactionExists(id);
                return Ok(exists);
            }

        }
    }
}
