namespace Trip.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationsController(IDestinationService destinationService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<List<Destination>>> GetDestinations()
    {
        return Ok(await destinationService.GetDestinationsAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User, Admin")]
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateDestination(Destination destination)
    {
        await destinationService.CreateDestinationAsync(destination);
        return CreatedAtAction(nameof(GetDestinationById), new { id = destination.Id }, destination);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateDestination(int id, Destination destination)
    {

        if (!await destinationService.DestinationExistsAsync(id))
        {
            return NotFound();
        }
        destination.Id = id;
        await destinationService.UpdateDestinationAsync(destination);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteDestination(int id)
    {
        if (!await destinationService.DestinationExistsAsync(id))
        {
            return NotFound();
        }
        await destinationService.DeleteDestinationAsync(id);
        return NoContent();
    }
}