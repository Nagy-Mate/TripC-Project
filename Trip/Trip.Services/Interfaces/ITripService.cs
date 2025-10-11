
namespace Trip.Services.Interfaces;

public interface ITripService
{
    Task<Data.DbModels.Trip?> GetTripByIdAsync(int id);
    Task<List<Data.DbModels.Trip>> GetTripsAsync();
}