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
}
