namespace Trip.Services.Interfaces;

public interface ITripService
{
    Task<List<Data.DbModels.Trip>> GetTripsAsync();
}