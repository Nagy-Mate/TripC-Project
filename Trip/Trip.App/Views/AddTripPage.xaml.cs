namespace Trip.App.Views;

public partial class AddTripPage : ContentPage
{
    public AddTripPageViewModel AddTripPageViewModel => BindingContext as AddTripPageViewModel;

    public AddTripPage(AddTripPageViewModel addTripPageViewModel)
    {
        BindingContext = addTripPageViewModel;
        InitializeComponent();
    }
}