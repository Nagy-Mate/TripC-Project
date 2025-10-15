namespace Trip.App.Views;

public partial class UpdateTripPage : ContentPage
{
    public UpdateTripPageViewModel UpdateTripPageViewModel => BindingContext as UpdateTripPageViewModel;

    public UpdateTripPage(UpdateTripPageViewModel updateTripPageViewModel)
    {
        BindingContext = updateTripPageViewModel;
        InitializeComponent();
    }
}