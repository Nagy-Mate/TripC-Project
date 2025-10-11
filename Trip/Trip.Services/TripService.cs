using Microsoft.EntityFrameworkCore;
using Trip.Data;
using Trip.Services.Interfaces;

namespace Trip.Services;

public class TripService(TripDbContext db) : ITripService
{
    public async Task<List<Data.DbModels.Trip>> GetTripsAsync()
    {
        return await db.Trips.ToListAsync();
    }

    public async Task<Data.DbModels.Trip?> GetTripByIdAsync(int id)
    {
        return await db.Trips.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Data.DbModels.Trip>> GetTripsByDestinationIdAsync(int destinationId)
    {
        return await db.Trips.Where(t => t.DestinationId == destinationId).ToListAsync();
    }

    public async Task CreateTripAsync(Data.DbModels.Trip trip)
    {
        db.Trips.Add(trip);
        await db.SaveChangesAsync();
    }

}
