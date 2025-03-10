using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropTypeController : ControllerBase
    {
        private readonly ICropType cropTypeRepository;
        public CropTypeController(ICropType _cropTypeRepository)
        {
            cropTypeRepository = _cropTypeRepository;
        }
        // ✅ Get all crop types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropType>>> GetAllCropTypes()
        {
            try
            {
                var cropTypes = await cropTypeRepository.GetAllCropTypes();
                return Ok(cropTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // ✅ Get a crop type by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CropType>> GetCropTypeById(int id)
        {
            try
            {
                var cropType = await cropTypeRepository.GetCropTypeById(id);
                if (cropType == null) return NotFound($"Crop Type with ID {id} not found.");
                return Ok(cropType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // ✅ Create a new crop type
        [HttpPost]
        public async Task<ActionResult> CreateCropType([FromBody] CropType cropType)
        {
            try
            {
                if (cropType == null) return BadRequest("Invalid crop type data.");

                var result = await cropTypeRepository.CreateCropType(cropType);
                if (result) return CreatedAtAction(nameof(GetCropTypeById), new { id = cropType.CropTypeId }, cropType);
                
                return StatusCode(500, "Error creating crop type.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // ✅ Update an existing crop type
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCropType(int id, [FromBody] CropType cropType)
        {
            try
            {
                if (cropType == null) return BadRequest("Invalid crop type data.");

                var result = await cropTypeRepository.UpdateCropType(id, cropType);
                if (result) return Ok("Crop Type updated successfully.");

                return NotFound($"Crop Type with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // ✅ Delete a crop type
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCropType(int id)
        {
            try
            {
                var result = await cropTypeRepository.DeleteCropType(id);
                if (result) return Ok("Crop Type deleted successfully.");

                return NotFound($"Crop Type with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //// ✅ Get recently added crop types (last X days)
        //[HttpGet("recent/{days}")]
        //public async Task<ActionResult<IEnumerable<CropType>>> GetRecentlyAddedCropTypes(int days)
        //{
        //    try
        //    {
        //        var cropTypes = await cropTypeRepository.GetRecentlyAddedCropTypes(days);
        //        return Ok(cropTypes);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

        // ✅ Get the most popular crop types
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<CropType>>> GetPopularCropTypes()
        {
            try
            {
                var cropTypes = await cropTypeRepository.GetPopularCropTypes();
                return Ok(cropTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
