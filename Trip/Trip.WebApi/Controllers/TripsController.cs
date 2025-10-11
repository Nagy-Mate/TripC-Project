using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trip.Data;
using Trip.Services;

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
