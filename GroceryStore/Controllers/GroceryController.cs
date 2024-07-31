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

        public GroceryController(IGroceryService groceryService)
        {
            _groceryService = groceryService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddEntities([FromBody] IEnumerable<EntityRequestDto> entities)
        {
            if (entities == null || !entities.Any())
            {
                return BadRequest(new { message = "Entities list cannot be null or empty!" });
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

        [HttpGet("changed-prices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetChangedPrices()
        {
            try
            {
                var changedPrices = await _groceryService.GetChangedPricesAsync();
                return Ok(changedPrices);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new { message = "A logical error occurred.", details = ex.Message });
            }
        }
    }
}
