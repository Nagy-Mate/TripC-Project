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
}
