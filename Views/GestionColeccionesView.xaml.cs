using TCGErcilla.ViewModels;

namespace TCGErcilla.Views;

public partial class GestionColeccionesView : ContentPage
{
    public GestionColeccionesViewModel ViewModel { get; set; }

    public GestionColeccionesView()
	{
		InitializeComponent();
        ViewModel = new GestionColeccionesViewModel();

    }
}