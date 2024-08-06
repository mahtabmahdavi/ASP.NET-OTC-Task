using GroceryStore.Data;
using GroceryStore.Models.Dtos;
using GroceryStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly IGroceryService _groceryService;
        private readonly TestDataGenerator _testDataGenerator;

        public GroceryController(IGroceryService groceryService, TestDataGenerator testDataGenerator)
        {
            _groceryService = groceryService;
            _testDataGenerator = testDataGenerator;
        }

        [HttpPost("generate-test-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateTestData([FromQuery] int count)
        {
            if (count <= 0)
            {
                return BadRequest(new { message = "Count must be greater than zero." });
            }

            try
            {
                await _testDataGenerator.GenerateTestDataAsync(count);
                return Ok(new { message = $"{count} test data records have been generated." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while generating test data." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddEntities([FromBody] IEnumerable<EntityDto> entities)
        {
            if (entities == null || !entities.Any())
            {
                return BadRequest(new { message = "Entity list cannot be null or empty!" });
            }

            try
            {
                await _groceryService.AddNewEntitiesAsync(entities);
                return Ok(new { message = "Entities added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("comparison")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComparison()
        {
            try
            {
                var comparisonResult = await _groceryService.GetComparisonAsync();
                return Ok(comparisonResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A logical error occurred." });
            }
        }
    }
}
