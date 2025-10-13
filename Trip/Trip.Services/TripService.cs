using Microsoft.EntityFrameworkCore;
using Trip.Data;
using Trip.Services.Interfaces;

namespace Trip.Services;

public class TripService(TripDbContext db) : ITripService
{
    public async Task<List<Data.DbModels.Trip>> GetTripsAsync()
    {
        return await db.Trips
            .Include(t => t.Destination)
            .ToListAsync();
    }


    public async Task<Data.DbModels.Trip?> GetTripByIdAsync(int id)
    {
        return await db.Trips
            .Include(t => t.Destination)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Data.DbModels.Trip>> GetTripsByDestinationIdAsync(int destinationId)
    {
        return await db.Trips.Where(t => t.DestinationId == destinationId).ToListAsync();
    }

    public async Task CreateTripAsync(Data.DbModels.Trip trip)
    {
        trip.Destination = null;
        db.Trips.Add(trip);
        await db.SaveChangesAsync();
    }


    public async Task<bool> TripExistsAsync(int id)
    {
        return await db.Trips.AnyAsync(t => t.Id == id);
    }

    public async Task DeleteTripAsync(int id)
    {
        await db.Trips.Where(t => t.Id == id).ExecuteDeleteAsync();
    }
    public async Task UpdateTripAsync(Data.DbModels.Trip trip)
    {
        await db.Trips.Where(t => t.Id == trip.Id)
            .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Name, trip.Name)
                .SetProperty(t => t.StartDate, trip.StartDate)
                .SetProperty(t => t.EndDate, trip.EndDate)
                .SetProperty(t => t.DestinationId, trip.DestinationId));
                
    }
}