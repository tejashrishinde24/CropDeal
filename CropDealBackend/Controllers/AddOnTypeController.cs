using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using CropDealBackend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnTypeController : ControllerBase
    {
        private readonly IAddonType _addOnTypeRepository;

        public AddOnTypeController(IAddonType addOnTypeRepository)
        {
            _addOnTypeRepository = addOnTypeRepository;
        }

        // ✅ Get all Add-on Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddOnType>>> GetAllAddonTypes()
        {
            var addonTypes = await _addOnTypeRepository.GetAllAddonTypes();
            return Ok(addonTypes);
        }

        // ✅ Get Add-on Type by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AddOnType>> GetAddonTypeById(int id)
        {
            var addonType = await _addOnTypeRepository.GetAddonTypeById(id);
            if (addonType == null)
                return NotFound(new { Message = "Add-on Type not found" });

            return Ok(addonType);
        }

        // ✅ Add new Add-on Type
        [HttpPost]
        public async Task<ActionResult> AddAddonType([FromBody] AddonTypeVM addonType)
        {
            if (addonType == null)
                return BadRequest(new { Message = "Invalid add-on type data" });

            var result = await _addOnTypeRepository.AddAddonType(addonType);
            if (result)
                return CreatedAtAction(nameof(GetAddonTypeById), new { id = addonType.AddOnTypeId }, addonType);

            return BadRequest(new { Message = "Failed to add add-on type" });
        }

        // ✅ Update Add-on Type
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddonType(int id, [FromBody] AddonTypeVM addonType)
        {
            if (addonType == null || id != addonType.AddOnTypeId)
                return BadRequest(new { Message = "Invalid add-on type data" });

            var result = await _addOnTypeRepository.UpdateAddonType(id,addonType);
            if (result)
                return Ok(new { Message = "Add-on Type updated successfully" });

            return NotFound(new { Message = "Add-on Type not found or update failed" });
        }

        // ✅ Delete Add-on Type
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddonType(int id)
        {
            var result = await _addOnTypeRepository.DeleteAddonType(id);
            if (result)
                return Ok(new { Message = "Add-on Type deleted successfully" });

            return NotFound(new { Message = "Add-on Type not found or deletion failed" });
        }

        // ✅ Get Add-on Types recently added in last X days
        //[HttpGet("recent/{days}")]
        //public async Task<ActionResult<IEnumerable<AddOnType>>> GetRecentlyAddedAddonTypes(int days)
        //{
        //    var addonTypes = await _addOnTypeRepository.GetRecentlyAddedAddonTypes(days);
        //    return Ok(addonTypes);
        //}

        // ✅ Get most popular Add-on Types
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<AddOnType>>> GetPopularAddonTypes()
        {
            var addonTypes = await _addOnTypeRepository.GetPopularAddonTypes();
            return Ok(addonTypes);
        }
    }
}
