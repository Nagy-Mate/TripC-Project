using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trip.Services.Interfaces;

namespace Trip.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController(ITripService tripService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Data.DbModels.Trip>>> GetTrips()
    {
        return Ok(await tripService.GetTripsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Data.DbModels.Trip>> GetTripById(int id)
    {          
        var trip = await tripService.GetTripByIdAsync(id);
        if (trip == null)
        {
            return NotFound();
        }
        return Ok(trip);
    }

    [HttpGet("ByDestination/{destinationId}")]
    public async Task<ActionResult<List<Data.DbModels.Trip>>> GetTripsByDestinationId(int destinationId)
    {
        return Ok(await tripService.GetTripsByDestinationIdAsync(destinationId));
    }   
    [HttpPost]
    public async Task<ActionResult> CreateTrip(Data.DbModels.Trip trip)
    {
        await tripService.CreateTripAsync(trip);
        return CreatedAtAction(nameof(GetTripById), new { id = trip.Id }, trip);
    }
}
