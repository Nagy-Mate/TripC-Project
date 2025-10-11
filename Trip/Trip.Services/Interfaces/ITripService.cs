


namespace Trip.Services.Interfaces;

public interface ITripService
{
    Task CreateTripAsync(Data.DbModels.Trip trip);
    Task<Data.DbModels.Trip?> GetTripByIdAsync(int id);
    Task<List<Data.DbModels.Trip>> GetTripsAsync();
    Task<List<Data.DbModels.Trip>> GetTripsByDestinationIdAsync(int destinationId);
}