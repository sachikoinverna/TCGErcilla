using TCGErcilla.Views;
using Microsoft.Maui.Controls;
namespace TCGErcilla
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginView", typeof(LoginView));
            Routing.RegisterRoute("MainPage", typeof(MainPage));


        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
