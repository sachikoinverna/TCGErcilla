using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty]
        private string urlWeb;
        [RelayCommand]
        public void GetLoginWeb()
        {
            UrlWeb = "https://www.google.es/"; 
        }
        [RelayCommand]

        public async Task MostrarMainPage()
        {
            try
            {
                await Shell.Current.GoToAsync("MainPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a MainPage: {ex.Message}");
            }

        }
    }
}
