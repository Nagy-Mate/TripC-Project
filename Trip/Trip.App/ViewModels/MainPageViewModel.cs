using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using Trip.Services.Interfaces;

namespace Trip.App.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private ObservableCollection<Data.DbModels.Trip> trips;

    public MainPageViewModel(ITripService tripService)
    {
        Trips = new ObservableCollection<Data.DbModels.Trip>();
        _httpClient = new HttpClient();
        LoadTripsAsync();
    }

    [RelayCommand]
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
}