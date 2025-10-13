using Trip.App.Views;

namespace Trip.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("AddDestinationPage", typeof(AddDestinationPage));
            Routing.RegisterRoute("AddTripPage", typeof(AddTripPage));
            InitializeComponent();
        }
    }
}