namespace Trip.Services;

public interface ITripService
{
    Task<List<Data.DbModels.Trip>> GetTripsAsync();
}