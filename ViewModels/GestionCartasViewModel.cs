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
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Views.Mopups;

namespace TCGErcilla.ViewModels
{
    public partial class GestionCartasViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CartaInfo> listaCartas = new ObservableCollection<CartaInfo>();
        [ObservableProperty]
        private CartaInfo selectedCarta;
        [ObservableProperty]
        private bool isColeccionInfoVisible;
        [ObservableProperty]
        private bool isReportsVisible;
        [RelayCommand]
        public void EstadoInicial()
        {
            IsColeccionInfoVisible = false;
            IsReportsVisible = false;
        }
        [RelayCommand]
        private void OcultarColeccion()
        {
            IsColeccionInfoVisible = false;
        }
        [RelayCommand]
        private void OcultarReports()
        {
            IsReportsVisible = false;
        }
        [RelayCommand]
        public void MostrarColeccion()
        {
            if (SelectedCarta == null)
            {
                IsColeccionInfoVisible = true;
                IsReportsVisible = false;
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            // if (SelectedColeccion .Gastos.Count > 0)
            // {
            //    IsCartasVisible = false;
            //   IsProductosVisible = true;
            //}
            //RequestModel request = new RequestModel()
            //{
            // Method = "GET",
            //  Route = "http://192.168.20.102:8080/colecciones/todas"
            //};

            // ResponseModel response = await APIService.ExecuteRequest(request);
            //if (response.Success.Equals(0))
            ///{
            // try
            //{
            //   ListaProductos =
            //     JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
            //}
            //catch (Exception ex)
            //{

            //}
            //}
            else
            {
            }
        }
        [RelayCommand]
        public void MostrarInformes()
        {
                IsReportsVisible = true;

                IsColeccionInfoVisible = false;

        }
        [RelayCommand]
        public async void GetCartas()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                //Route = "http://localhost:8080/cartas/todas"
               Route = "http://192.168.20.102:8080/cartas/todas"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaCartas =
                JsonConvert.DeserializeObject<ObservableCollection<CartaInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task CrearCarta()
        {
            await MopupService.Instance.PushAsync(new CartaFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarCarta()
        {
            var mopup = new CartaFormularioMopup();
            var vm = new CartaFormularioViewModel();
            vm.CartaInfo = (CartaInfo)SelectedCarta.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarCarta()
        {
            if (SelectedCarta != null) {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://192.168.20.102:8080/cartas/borrar/" + SelectedCarta.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
    }
}
}
