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
        [ObservableProperty]
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<CartaInfo> listaCartas = new ObservableCollection<CartaInfo>();
        [ObservableProperty]
        private bool isCartasVisible;
        [ObservableProperty]
        private bool isProductosVisible;
        [ObservableProperty]
        private bool isReportesVisible;
        [RelayCommand]
        public async Task MostrarCartas()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            else
            {
                IsProductosVisible = false;
                IsCartasVisible = true;
                IsReportesVisible = false;
                RequestModel request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://192.168.20.102:8080/cartas/todas"
                };

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                    try
                    {
                        ListaProductos =
                          JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        [RelayCommand]
        public void EstadoInicial()
        {
            IsProductosVisible = false;
            IsCartasVisible = false;
            IsReportesVisible = false;
        }
        [RelayCommand]
        private void OcultarProductos()
        {
            IsProductosVisible = false;
        }
        [RelayCommand]
        private void OcultarCartas()
        {
            IsCartasVisible = false;
        }
        [RelayCommand]
        private void OcultarReportes()
        {
            IsReportesVisible = false;
        }
        [RelayCommand]
        public async Task MostrarProductos()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            else
            {
                IsProductosVisible = true;
                IsCartasVisible= false;
                IsReportesVisible = false;
                RequestModel request = new RequestModel()
            {
             Method = "GET",
              Route = "http://192.168.20.102:8080/productos/todos"
            };

                 ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                 try
                {
                   ListaProductos =
                     JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {

                }
                }
            }
        }
        [RelayCommand]
        public void MostrarReportes()
        {
            IsReportesVisible = true;
            IsProductosVisible = false;
            IsCartasVisible = false;
        }
        [RelayCommand]
        public async void GetColecciones()
        {


            RequestModel request = new RequestModel()
            {
                Method = "GET",
                 //Route = "http://192.168.1.136:8080/colecciones/todas"
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
            vm.ColeccionInfo = (ColeccionInfo)SelectedColeccion.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarColeccion()
        {
            if (SelectedColeccion != null)
            {
                var request = new RequestModel()
                {
                    Method = "GET",
               
                    Route = "http://192.168.20.102:8080/colecciones/borrar/" + SelectedColeccion.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
              //  await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
