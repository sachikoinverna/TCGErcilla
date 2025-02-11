using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.ViewModels
{
    public partial class DistribuidoresViewModel:ObservableObject
    {
        [RelayCommand]
        public async Task GoDistribuidorCrear()
        {
            try
            {
                await Shell.Current.GoToAsync("DistribuidorFormularioView");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a MainPage: {ex.Message}");
            }
        }
    }
}
