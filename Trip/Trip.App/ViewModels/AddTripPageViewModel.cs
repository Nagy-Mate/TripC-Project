using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using Trip.Data.DbModels;

namespace Trip.App.ViewModels;

public partial class AddTripPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private ObservableCollection<Destination> destinations;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private DateTime startDate;

    [ObservableProperty]
    private DateTime endDate;

    [ObservableProperty]
    private Destination selectedDestination;

    public AddTripPageViewModel()
    {
        _httpClient = new HttpClient();
        Destinations = new ObservableCollection<Destination>();
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
        LoadDestinations();
    }

    private async Task LoadDestinations()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<Destination>>("http://localhost:5048/api/destinations");

            if (response != null)
            {
                Destinations = new ObservableCollection<Destination>(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [RelayCommand]
    private async Task AddTrip()
    {
        var selectedDestination = this.SelectedDestination;

        var newTrip = new Data.DbModels.Trip
        {
            Name = this.Name,
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            Destination = selectedDestination,
            DestinationId = selectedDestination.Id
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5048/api/trips", newTrip);

            Name = string.Empty;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            SelectedDestination = null;
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.ToString());
        }
    }
}