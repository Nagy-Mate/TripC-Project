using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using Trip.Data.DbModels;

namespace Trip.App.ViewModels;

public partial class UpdateTripPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private ObservableCollection<Data.DbModels.Trip> trips;

    [ObservableProperty]
    private ObservableCollection<Destination> destinations;

    [ObservableProperty]
    private Data.DbModels.Trip selectedTripForUpdate;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private DateTime startDate;

    [ObservableProperty]
    private DateTime endDate;

    [ObservableProperty]
    private Destination selectedDestination;

    public UpdateTripPageViewModel()
    {
        _httpClient = new HttpClient();
        Trips = new ObservableCollection<Data.DbModels.Trip>();
        Destinations = new ObservableCollection<Destination>();
        LoadDestinations();
        LoadTripsAsync();
    }

    partial void OnSelectedTripForUpdateChanged(Data.DbModels.Trip value)
    {
        if (value != null)
        {
            Name = value.Name;
            StartDate = value.StartDate;
            EndDate = value.EndDate;
            SelectedDestination = value.Destination;
        }
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

    private async void LoadTripsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<Data.DbModels.Trip>>("http://localhost:5048/api/trips");

            if (response != null)
            {
                Trips.Clear();
                foreach (var trip in response)
                {
                    Trips.Add(trip);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba az adatok lekérésekor: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task UpdateTrip()
    {
        if (SelectedTripForUpdate == null)
        {
            Console.WriteLine("Nincs kiválasztva út a frissítéshez.");
            return;
        }

        var selectedDestination = SelectedDestination;

        var updatedTrip = new Data.DbModels.Trip
        {
            Id = SelectedTripForUpdate.Id,
            Name = this.Name,
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            Destination = selectedDestination,
            DestinationId = selectedDestination.Id
        };

        try
        {
            var response = await _httpClient.PutAsJsonAsync("http://localhost:5048/api/trips/"+SelectedTripForUpdate.Id, updatedTrip);

            if (response.IsSuccessStatusCode)
            {
                var index = Trips.IndexOf(SelectedTripForUpdate);
                if (index != -1)
                {
                    Trips[index] = updatedTrip;
                }

                SelectedTripForUpdate = null;
                Name = string.Empty;
                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
                SelectedDestination = null;
            }
            else
            {
                Console.WriteLine($"Hiba a frissítéskor: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba a frissítés elküldésekor: {ex.Message}");
        }
    }
}