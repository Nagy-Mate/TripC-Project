namespace Trip.Data;

public class TripDbContext(DbContextOptions<TripDbContext> options) : DbContext(options)
{
    public DbSet<DbModels.Trip> Trips { get; set; }
    public DbSet<Destination> Destinations { get; set; }
}