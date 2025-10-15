namespace Trip.App;

public partial class MainPage : ContentPage
{
    public MainPageViewModel MainPageViewModel => BindingContext as MainPageViewModel;
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;
        InitializeComponent();
    }
}