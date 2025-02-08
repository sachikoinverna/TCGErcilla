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

        // Ocultar el bot�n de atr�s en esta p�gina
        var currentPage = Shell.Current.CurrentPage;
        if (currentPage != null)
        {
            Shell.SetBackButtonBehavior(currentPage, new BackButtonBehavior
            {
                IsVisible = false // Ocultamos el bot�n de atr�s
            });
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Restaurar el bot�n de atr�s cuando salimos de esta p�gina
        var currentPage = Shell.Current.CurrentPage;
        if (currentPage != null)
        {
            Shell.SetBackButtonBehavior(currentPage, new BackButtonBehavior
            {
                IsVisible = true // Restauramos el bot�n de atr�s
            });
        }
    }
}
