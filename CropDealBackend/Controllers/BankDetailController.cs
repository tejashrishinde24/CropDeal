using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankDetailController : ControllerBase
    {
        private readonly IBankDetail service;

        public BankDetailController(IBankDetail _service)
        {
            service = _service;
        }

        [HttpPost("AddBankDetails")]
        public async Task<IActionResult> AddBankDetail([FromBody] BankDetailVM bankDetail)
        {
            if (bankDetail == null)
                return BadRequest("Invalid bank details.");

            var result = await service.AddBankDetail(bankDetail);
            if (!result) return Conflict("Bank detail with the same ID already exists.");

            return Ok("Bank detail added successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankDetail(int id)
        {
            var result = await service.DeleteBankDetail(id);
            if (!result) return NotFound("Bank detail not found.");

            return Ok("Bank detail deleted successfully.");
        }

        [HttpGet("GetAllBankDetails")]
        public async Task<IActionResult> GetAllBankDetails()
        {
            var bankDetails = await service.GetAllBankDetails();
            return Ok(bankDetails);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllBankDetailsByUserId(int userId)
        {
            var bankDetails = await service.GetAllBankDetailsByUserId(userId);
            return Ok(bankDetails);
        }

        [HttpGet("account/{accountNumber}")]
        public async Task<IActionResult> GetBankDetailByAccountNumber(string accountNumber)
        {
            var bankDetail = await service.GetBankDetailByAccountNumber(accountNumber);
            if (bankDetail == null) return NotFound("Bank detail not found.");
            return Ok(bankDetail);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankDetailById(int id)
        {
            var bankDetail = await service.GetBankDetailById(id);
            if (bankDetail == null) return NotFound("Bank detail not found.");
            return Ok(bankDetail);
        }

        [HttpPut("UpdatedBank")]
        public async Task<IActionResult> UpdateBankDetail([FromBody] BankDetailVM bankDetail)
        {
            if (bankDetail == null) return BadRequest("Invalid bank details.");

            var result = await service.UpdateBankDetail(bankDetail);
            if (!result) return NotFound("Bank detail not found.");

            return Ok("Bank detail updated successfully.");
        }




        [HttpGet("bank/{bankName}")]
        public async Task<IActionResult> GetBankDetailsByBankName(string bankName)
        {
            var bankDetails = await service.GetBankDetailsByBankName(bankName);
            return Ok(bankDetails);
        }

        [HttpGet("distinct-banks")]
        public async Task<IActionResult> GetDistinctBankNames()
        {
            var bankNames = await service.GetDistinctBankNames();
            return Ok(bankNames);
        }

        [HttpGet("total-accounts")]
        public async Task<IActionResult> GetTotalBankAccounts()
        {
            var total = await service.GetTotalBankAccounts();
            return Ok(total);
        }

        [HttpGet("user-has-account/{userId}")]
        public async Task<IActionResult> UserHasBankAccount(int userId)
        {
            var hasAccount = await service.UserHasBankAccount(userId);
            return Ok(hasAccount);
        }
    }
}
