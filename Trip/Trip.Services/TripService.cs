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
}
