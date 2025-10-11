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
}
