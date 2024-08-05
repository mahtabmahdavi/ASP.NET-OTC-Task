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
        public async Task<IActionResult> AddEntities([FromBody] IEnumerable<EntityDto> entities)
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChangedPrices()
        {
            try
            {
                var changedEntities = await _groceryService.GetChangedPricesAsync();
                return Ok(changedEntities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A logical error occurred." });
            }
        }

        [HttpGet("removed-entities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRemovedEntities()
        {
            try
            {
                var removedEntities = await _groceryService.GetRemovedEntitiesAsync();
                return Ok(removedEntities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A logical error occurred." });
            }
        }

        [HttpGet("added-entities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddedEntities()
        {
            try
            {
                var addedEntities = await _groceryService.GetAddedEntitiesAsync();
                return Ok(addedEntities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A logical error occurred." });
            }
        }
    }
}
