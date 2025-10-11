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
}
