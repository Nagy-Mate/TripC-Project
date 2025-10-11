using Microsoft.AspNetCore.Mvc;
using Trip.Data.DbModels;
using Trip.Services.Interfaces;

namespace Trip.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationsController(IDestinationService destinationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Destination>>> GetDestinations()
    {
        return Ok(await destinationService.GetDestinationsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestinationById(int id)
    {
        var destination = await destinationService.GetDestinationByIdAsync(id);
        if (destination == null)
        {
            return NotFound();
        }
        return Ok(destination);
    }
    [HttpPost]
    public async Task<ActionResult> CreateDestination(Destination destination)
    {
        await destinationService.CreateDestinationAsync(destination);
        return CreatedAtAction(nameof(GetDestinationById), new { id = destination.Id }, destination);
    }   
}
