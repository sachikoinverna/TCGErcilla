using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.ViewModels
{
    public partial class CartasViewModel:ObservableObject
    {
        [RelayCommand]
        public async Task GoCartaCrear()
        {
            try
            {
                await Shell.Current.GoToAsync("CartaFormularioView");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a MainPage: {ex.Message}");
            }
        }
    }
}
