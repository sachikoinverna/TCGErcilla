using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.ViewModels
{
    public partial class MainViewModel:ObservableObject 
    {
        [ObservableProperty]
        private bool loginIniciado;
        [RelayCommand]
        public async Task MostrarLogin()
        {
            try
            {
                await Shell.Current.GoToAsync("//LoginView");
            }catch(Exception ex)
            {
                Console.WriteLine($"Error al navegar a LoginView: {ex.Message}");
            }
        }
    }
}
