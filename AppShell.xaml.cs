using TCGErcilla.Views;
using TCGErcilla.Views.Formularios;
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
            Routing.RegisterRoute("CartaFormularioView", typeof(CartaFormularioView));
            Routing.RegisterRoute("GestionDistribuidoresView", typeof(GestionDistribuidoresView));
            Routing.RegisterRoute("GestionCartasView", typeof(GestionCartasView));
            Routing.RegisterRoute("DistribuidorFormularioView",typeof(DistribuidorFormularioView));
            Routing.RegisterRoute("ProductoFormularioView", typeof(ProductoFormularioView));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
