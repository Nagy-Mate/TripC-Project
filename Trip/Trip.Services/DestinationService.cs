using Microsoft.EntityFrameworkCore;
using Trip.Data;
using Trip.Data.DbModels;
using Trip.Services.Interfaces;

namespace Trip.Services;

public class DestinationService(TripDbContext db) : IDestinationService
{

    public async Task<List<Destination>> GetDestinationsAsync()
    {
        return await db.Destinations.Include(d => d.Trips).ToListAsync();
    }   
    public async Task<Destination?> GetDestinationByIdAsync(int id)
    {
        return await db.Destinations.Include(d => d.Trips).FirstOrDefaultAsync(d => d.Id == id);
    }   
    public async Task CreateDestinationAsync(Destination destination)
    {
        db.Destinations.Add(destination);
        await db.SaveChangesAsync();
    }
    public async Task<bool> DestinationExistsAsync(int id)
    {
        return await db.Destinations.AnyAsync(d => d.Id == id);
    }
    public async Task DeleteDestinationAsync(int id)
    {
        await db.Destinations.Where(d => d.Id == id).ExecuteDeleteAsync();
    }

    public async Task UpdateDestinationAsync(Destination destination)
    {
        await db.Destinations.Where(d => d.Id == destination.Id)
            .ExecuteUpdateAsync(d => d
                .SetProperty(d => d.Name, destination.Name)
                .SetProperty(d => d.Country, destination.Country)
                .SetProperty(d => d.Description, destination.Description));
    }
}
