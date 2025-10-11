



namespace Trip.Services.Interfaces;

public interface ITripService
{
    Task CreateTripAsync(Data.DbModels.Trip trip);
    Task DeleteTripAsync(int id);
    Task<Data.DbModels.Trip?> GetTripByIdAsync(int id);
    Task<List<Data.DbModels.Trip>> GetTripsAsync();
    Task<List<Data.DbModels.Trip>> GetTripsByDestinationIdAsync(int destinationId);
    Task<bool> TripExistsAsync(int id);
    Task UpdateTripAsync(Data.DbModels.Trip trip);
}