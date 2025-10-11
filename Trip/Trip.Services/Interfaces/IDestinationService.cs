using Trip.Data.DbModels;

namespace Trip.Services.Interfaces
{
    public interface IDestinationService
    {
        Task CreateDestinationAsync(Destination destination);
        Task<Destination?> GetDestinationByIdAsync(int id);
        Task<List<Destination>> GetDestinationsAsync();
    }
}