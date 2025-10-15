namespace Trip.App.Views;

public partial class AddDestinationPage : ContentPage
{
    public AddDestinationPageViewModel AddDestinationPageViewModel => BindingContext as AddDestinationPageViewModel;

    public AddDestinationPage(AddDestinationPageViewModel addDestinationPageViewModel)
    {
        BindingContext = addDestinationPageViewModel;
        InitializeComponent();
    }
}