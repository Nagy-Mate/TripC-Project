using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;
using Trip.Data.DbModels;

namespace Trip.App.ViewModels;

public partial class AddDestinationPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string country;

    [ObservableProperty]
    private string description;

    public AddDestinationPageViewModel()
    {
        _httpClient = new HttpClient();
    }

    [RelayCommand]
    private async Task AddDestination()
    {
        var newDestination = new Destination
        {
            Name = this.Name,
            Country = this.Country,
            Description = this.Description,
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5048/api/destinations", newDestination);

            Name = string.Empty;
            Country = string.Empty;
            Description = string.Empty;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Hiba az adatok küldésekor: {ex.Message}");
        }
    }
}