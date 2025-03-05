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
        [ObservableProperty]
        private bool isColeccionesVisible;
        [ObservableProperty]
        private bool isImagenVisible;
        [ObservableProperty]
        private string urlPDF; 

        [ObservableProperty]
        private string filtroNombre; 
        [ObservableProperty]
        private DateTime filtroFechaDesde;
        [ObservableProperty]
        private DateTime filtroFechaHasta;
        [RelayCommand]
        public void GetPDFNombre()
        {
            UrlPDF = "http://localhost:8082/report/getReportColeccionesByNombre/" + FiltroNombre;
        }

        [RelayCommand]
        public void GetPDFechas()
        {
            if (FiltroFechaDesde != null && FiltroFechaHasta != null)
            {
                if (FiltroFechaHasta > FiltroFechaDesde)
                {
                    string fechaJasperDesde = FiltroFechaDesde.ToString("yyyy-MM-dd");
                    string fechaJasperHasta = FiltroFechaHasta.ToString("yyyy-MM-dd");

                    UrlPDF = "http://localhost:8082/report/getReportColeccionesByFechas/" + fechaJasperDesde + "/" + fechaJasperHasta;
                }else if(FiltroFechaHasta == FiltroFechaDesde)
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "Las fechas no deben ser iguales.", "Aceptar");

                }else if (FiltroFechaHasta < FiltroFechaDesde)
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "El campo 'Fecha desde' no debe ser mayor que el campo 'Fecha hasta'", "Aceptar");

                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar dos fechas", "Aceptar");

            }
        }
        [RelayCommand]
        public async Task MostrarCartas()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");
                return;
            }
            else
            {
                if (SelectedColeccion.Id != 0)
                {
                    IsProductosVisible = false;
                    IsCartasVisible = true;
                    IsImagenVisible =false;
                    IsReportesVisible = false;
                    RequestModel request = new RequestModel()
                    {
                        Method = "GET",
                        Route = "http://localhost:8080/cartas/buscarPorColeccionId/" + SelectedColeccion.Id
                    };

                    ResponseModel response = await APIService.ExecuteRequest(request);
                    if (response.Success.Equals(0))
                    {
                        try
                        {
                            ListaCartas =
                              JsonConvert.DeserializeObject<ObservableCollection<CartaInfo>>(response.Data.ToString());
                        }
                        catch (Exception ex)
                        {
                            await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");

                        }
                    }
                }
            }
        }
        [RelayCommand]
        public void EstadoInicial()
        {
            IsColeccionesVisible = false;
            IsProductosVisible = false;
            IsCartasVisible = false;
            IsReportesVisible = false;
            IsImagenVisible = true;
            FiltroFechaDesde = DateTime.Now;
            FiltroFechaHasta = DateTime.Now;

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
            IsImagenVisible = true;

        }
        [RelayCommand]
        private void OcultarColecciones()
        {
            IsColeccionesVisible = false;
            IsImagenVisible = true;

        }
        [RelayCommand]
        public async void MostrarProductos()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");
                return;
            }
            else
            {
                if (SelectedColeccion.Id != 0)
                {
                    IsProductosVisible = true;
                    IsCartasVisible = false;
                    IsReportesVisible = false;
                    IsImagenVisible = false;

                    RequestModel request = new RequestModel()
                    {
                        Method = "GET",
                        Route = "http://localhost:8080/productos/buscar/coleccion/" + SelectedColeccion.Id
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
                            await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");


                        }
                    }
                }
            }
        }
        [RelayCommand]
        public void MostrarColecciones()
        {
            GetColecciones();
            IsColeccionesVisible = true;
            IsImagenVisible = false;

            IsReportesVisible = false;
            IsProductosVisible = false;
            IsCartasVisible = false;
        }
        [RelayCommand]
        public void MostrarReportes()
        {
            UrlPDF = "http://localhost:8082/report/getReportColeccionesAll";
            IsColeccionesVisible = false;
            IsImagenVisible = false;

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
                 Route = "http://localhost:8080/colecciones/todas"
                 //Route = "http://192.168.20.102:8080/colecciones/todas"
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
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");


                }
            }
        }
        [RelayCommand]
        public async Task CrearColeccion()
        {
            var mopup = new ColeccionFormularioMopup();
            var vm = new ColeccionFormularioViewModel();
            vm.IsEditMode = false;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EditarColeccion()
        {
            var mopup = new ColeccionFormularioMopup();
            var vm = new ColeccionFormularioViewModel();
            vm.ColeccionInfo = (ColeccionInfo)SelectedColeccion.Clone();
            vm.IsEditMode = true;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
    }
}
