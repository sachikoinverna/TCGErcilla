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
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();

        [ObservableProperty]
        private CartaInfo selectedCarta;
        [ObservableProperty]
        private bool isColeccionInfoVisible;
        [ObservableProperty]
        private bool isReportsVisible;
        [ObservableProperty]
        private bool isCartasVisible;
        [ObservableProperty]
        private bool isImagenVisible;
        [ObservableProperty]
        private string urlPDF; 

        [ObservableProperty]
        private string filtroNombre; 
        [ObservableProperty]
        private ColeccionInfo filtroColeccion;
        [RelayCommand]
        public void GetPDFNombre()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportCartasByNombre/" + FiltroNombre;
        }
        [RelayCommand]
        public void GetPDFColeccion()
        {

            if (FiltroColeccion != null)
            {
                UrlPDF = "http://erciapps.sytes.net:11015/report/getReportCartasByIdColeccion/" + FiltroColeccion.Id;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");

            }
        }
        [RelayCommand]
        public async Task EstadoInicial()
        {
            IsColeccionInfoVisible = false;
            IsReportsVisible = false;
            IsCartasVisible = false;
            IsImagenVisible = true;
        }
        [RelayCommand]
        private void OcultarColeccion()
        {
            IsColeccionInfoVisible = false;
            IsReportsVisible = false;
        }
        [RelayCommand]
        private void OcultarReports()
        {
            IsReportsVisible = false;
            IsColeccionInfoVisible = false;
            IsCartasVisible = false;
            IsImagenVisible = true;
            FiltroNombre = null;
            FiltroColeccion = null;
       
        }
        [RelayCommand]
        public void MostrarColeccion()
        {
            if (SelectedCarta != null)
            {
                
                if (SelectedCarta.SelectedColeccion.Id != 0)
                {
                    IsImagenVisible = false;
                    IsColeccionInfoVisible = true;
                    IsReportsVisible = false;

                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una carta", "Aceptar");
                    return;
                }
                }
            }
        [RelayCommand]
        private void OcultarCartas()
        {
            IsReportsVisible = false;
            IsColeccionInfoVisible = false;
            IsCartasVisible = false;
            IsImagenVisible = true;

        }
        [RelayCommand]
        public void MostrarCartas()
        {
            GetCartas();
            IsImagenVisible = false;

            IsColeccionInfoVisible = false;
                    IsReportsVisible = false;
            IsCartasVisible = true;

        }
        [RelayCommand]
        public void MostrarInformes()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportCartasAll";
            GetListaColecciones();
                IsReportsVisible = true;
            IsCartasVisible =false;
            IsImagenVisible = false;

            IsColeccionInfoVisible = false;

        }
        [RelayCommand]
        public async void GetCartas()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://erciapps.sytes.net:11014/cartas/todas"
              
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
        public async void GetListaColecciones()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://erciapps.sytes.net:11014/colecciones/todas"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaColecciones =
                JsonConvert.DeserializeObject<ObservableCollection<ColeccionInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task CrearCarta()
        {
            var mopup = new CartaFormularioMopup();
            var vm = new CartaFormularioViewModel();
            vm.IsEditMode = false;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EditarCarta()
        {
            var mopup = new CartaFormularioMopup();
            var vm = new CartaFormularioViewModel();
            vm.CartaInfo = (CartaInfo)SelectedCarta.Clone();
            vm.IsEditMode = true;
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
                    Route = "http://erciapps.sytes.net:11014/cartas/borrar/" + SelectedCarta.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
    }
}
}
