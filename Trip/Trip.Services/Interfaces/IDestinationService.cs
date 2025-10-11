using Trip.Data.DbModels;

namespace Trip.Services.Interfaces
{
    public interface IDestinationService
    {
        Task<List<Destination>> GetDestinationsAsync();
    }
}