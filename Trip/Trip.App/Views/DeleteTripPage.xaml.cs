namespace Trip.App.Views;

public partial class DeleteTripPage : ContentPage
{
    public DeleteTripPageViewModel DeleteTripPageViewModel => BindingContext as DeleteTripPageViewModel;
    public DeleteTripPage(DeleteTripPageViewModel deleteTripPageViewModel)
    {
        BindingContext = deleteTripPageViewModel;
        InitializeComponent();
    }
}