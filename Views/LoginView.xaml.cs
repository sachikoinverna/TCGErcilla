using Microsoft.Maui.Controls;

namespace TCGErcilla.Views;

public partial class LoginView : ContentPage
{

    public LoginView()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Ocultar el botón de atrás en esta página
        var currentPage = Shell.Current.CurrentPage;
        if (currentPage != null)
        {
            Shell.SetBackButtonBehavior(currentPage, new BackButtonBehavior
            {
                IsVisible = false // Ocultamos el botón de atrás
            });
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Restaurar el botón de atrás cuando salimos de esta página
        var currentPage = Shell.Current.CurrentPage;
        if (currentPage != null)
        {
            Shell.SetBackButtonBehavior(currentPage, new BackButtonBehavior
            {
                IsVisible = true // Restauramos el botón de atrás
            });
        }
    }
}
