using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Trip.Data;

public class TripDbContext(DbContextOptions<TripDbContext> options) : IdentityDbContext(options)
{
    public DbSet<DbModels.Trip> Trips { get; set; }
    public DbSet<Destination> Destinations { get; set; }
}