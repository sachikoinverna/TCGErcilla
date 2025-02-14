using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;
using TCGErcilla.Views.Mopups;

namespace TCGErcilla.ViewModels
{
    public partial class ColeccionesViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ColeccionInfo selectedColeccionInfo;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private int numCarta;
        [ObservableProperty]
        private DateTime fecha_lanzamiento = DateTime.Today;
        [RelayCommand]
        public async void GetColecciones()
        {
            for (int i = 0; i < 5; i++)
            {
                listaColecciones.Add(new ColeccionInfo(i, "a", i));
            }

            // RequestModel request = new RequestModel()
            //{
            //   Method = "GET",
            //  Data = string.Empty,
            // Route = "http://localhost:8080/personas/todos"
            //};

            //ResponseModel response = await APIService.ExecuteRequest(request);
            // if (response.Success.Equals(0))
            //{
            //  try
            //{
            //    vm.ListaPersonas =
            //       JsonConvert.DeserializeObject<ObservableCollection<Persona>>(response.Data.ToString());
            // }
            //catch (Exception ex) { }
        }
        [RelayCommand]
        public async Task AbrirMopupCarta()
        {
            await MopupService.Instance.PushAsync(new ColeccionFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarDistribuidor()
        {
            //await Shell.Current.GoToAsync("View",
              //  new Dictionary<string, object>()
               // {
                //    ["CartaInfo"] = SelectedColeccionInfo
               // });
        }
    }
}
