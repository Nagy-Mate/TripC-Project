using Trip.Data.DbModels;

namespace Trip.Services.Interfaces
{
    public interface IDestinationService
    {
        Task<Destination?> GetDestinationByIdAsync(int id);
        Task<List<Destination>> GetDestinationsAsync();
    }
}