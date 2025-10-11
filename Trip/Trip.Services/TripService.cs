using Microsoft.EntityFrameworkCore;
using Trip.Data;

namespace Trip.Services;

public class TripService(TripDbContext db) : ITripService
{
    public async Task<List<Data.DbModels.Trip>> GetTripsAsync()
    {
        return await db.Trips.ToListAsync();
    }
}
