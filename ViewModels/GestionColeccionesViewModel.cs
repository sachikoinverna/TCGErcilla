using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Views.Mopups;

namespace TCGErcilla.ViewModels
{
    public partial class GestionColeccionesViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ColeccionInfo selectedColeccion;

        [RelayCommand]
        public async void GetColecciones()
        {


            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://192.168.20.102:8080/colecciones/todas"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
           {
                try
               {
                    ListaColecciones =
                       JsonConvert.DeserializeObject<ObservableCollection<ColeccionInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }
        [RelayCommand]
        public async Task CrearColeccion()
        {
            await MopupService.Instance.PushAsync(new ColeccionFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarColeccion()
        {
            var mopup = new ColeccionFormularioMopup();
            var vm = new ColeccionFormularioViewModel();
           // vm.ColeccionInfo = (ColeccionInfo)SelectedColeccion.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarColeccion()
        {
            if (SelectedColeccion != null)
            {
                //var _producto = new ProductoDto();
                //if (ProductoInfo.Id != null)
                //{
                // _producto.Id = ProductoInfo.Id;
                //_producto.Nombre = ProductoInfo.Nombre;
                //_producto.UrlImagen = ProductoInfo.UrlImagen;
                //_producto.FechaLanzamiento = ProductoInfo.FechaLanzamiento;
                var request = new RequestModel()
                {
                    Data = SelectedColeccion.Id,
                    Method = "POST",
                    Route = "http://localhost:8080/cartas/crear"
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
              //  await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
