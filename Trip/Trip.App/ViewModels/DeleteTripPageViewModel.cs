namespace Trip.App.ViewModels;

public partial class DeleteTripPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private ObservableCollection<Data.DbModels.Trip> trips;

    [ObservableProperty]
    private Data.DbModels.Trip selectedTripForDelete;

    public DeleteTripPageViewModel()
    {
        _httpClient = new HttpClient();
        Trips = new ObservableCollection<Data.DbModels.Trip>();
        LoadTripsAsync();
    }

    private async Task LoadTripsAsync()
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
    private async Task DeleteTrip()
    {
        if (SelectedTripForDelete != null)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5048/api/trips/{SelectedTripForDelete.Id}");

                if (response.IsSuccessStatusCode)
                {
                    Trips.Remove(SelectedTripForDelete);

                    SelectedTripForDelete = null;
                }
                else
                {
                    Console.WriteLine($"Hiba a törléskor: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a törlési kérés elküldésekor: {ex.Message}");
            }
        }
    }
}