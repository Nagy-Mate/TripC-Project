namespace Trip.Services.Interfaces;

public interface IDestinationService
{
    Task CreateDestinationAsync(Destination destination);
    Task DeleteDestinationAsync(int id);
    Task<bool> DestinationExistsAsync(int id);
    Task<Destination?> GetDestinationByIdAsync(int id);
    Task<List<Destination>> GetDestinationsAsync();
    Task UpdateDestinationAsync(Destination destination);
}