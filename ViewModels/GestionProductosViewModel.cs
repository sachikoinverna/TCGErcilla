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
    public partial class GestionProductosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ObservableCollection<TipoProductoInfo> listaTipoInfo = new ObservableCollection<TipoProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<DistribuidorInfo> listaDistribuidores = new ObservableCollection<DistribuidorInfo>();
        [ObservableProperty]
        private ProductoInfo selectedProducto;
        [ObservableProperty]
        private bool isDistribuidoresVisible;
        [ObservableProperty]
        private bool isReportesVisible;
        [ObservableProperty]
        private bool isImagenVisible;
        [ObservableProperty]
        private bool isProductosVisible;
        [ObservableProperty]
        private string urlPDF;
        [ObservableProperty]
        private string filtroNombre;
        [ObservableProperty]
        private ColeccionInfo filtroColeccion;
        [ObservableProperty]
        private DistribuidorInfo filtroDistribuidor;
        [ObservableProperty]
        private TipoProductoInfo filtroTipoProducto;
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
            IsImagenVisible = true;
            IsProductosVisible = false;

        }

        [RelayCommand]
        public void GetPDFNombre()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportProductosByNombre/" + FiltroNombre;
        }


        [RelayCommand]
        public void GetPDFColeccion()
        {

            if (FiltroColeccion != null)
            {
                UrlPDF = "http://erciapps.sytes.net:11015/report/getReportProductosByIdColeccion/" + FiltroColeccion.Id;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");

            }
        }

        [RelayCommand]
        public void GetPDFTipoProducto()
        {

            if (FiltroTipoProducto != null)
            {
                UrlPDF = "http://erciapps.sytes.net:11015/report/getReportProductosByIdTipoProducto/" + FiltroTipoProducto.Id;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un tipo producto", "Aceptar");

            }
        }




        [RelayCommand]
        public void GetPDFDistribuidor()
        {

            if (FiltroDistribuidor != null)
            {
                UrlPDF = "http://erciapps.sytes.net:11015/report/getReportProductosByIdDistribuidor/" + FiltroDistribuidor.Id;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un distribuidor", "Aceptar");

            }
        }


        [RelayCommand]
        public void MostrarReportes()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportProductosAll";
            GetListaColecciones();
            GetListaDistribuidores();
            GetListaTipoProducto();
            IsReportesVisible = true;
            IsDistribuidoresVisible = false;
            IsImagenVisible = false;
            IsProductosVisible = false;

        }
        [RelayCommand]
        public void OcultarReportes()
        {
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
            IsImagenVisible = true;
            IsProductosVisible = false;

        }
        [RelayCommand]
        public void MostrarProductos()
        {
          
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
            IsImagenVisible = false;
            IsProductosVisible = true;

        }
        [RelayCommand]
        public void OcultarProductos()
        {
          
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
            IsImagenVisible = true;
            IsProductosVisible = false;

        }
        [RelayCommand]
        public void MostrarDistribuidores()
        {
            IsReportesVisible = false;
            IsDistribuidoresVisible = true;
        }
        [RelayCommand]
        public void OcultarDistribuidores()
        {
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
            FiltroColeccion = null;
            FiltroDistribuidor = null;
            FiltroTipoProducto = null;
            FiltroNombre = null;
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
        public async void GetListaTipoProducto()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://erciapps.sytes.net:11014/tipo_producto/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaTipoInfo =
                JsonConvert.DeserializeObject<ObservableCollection<TipoProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }







        [RelayCommand]
        public async void GetListaDistribuidores()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://erciapps.sytes.net:11014/distribuidores/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaDistribuidores =
                JsonConvert.DeserializeObject<ObservableCollection<DistribuidorInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }






        [RelayCommand]
        public async void GetProductos() 
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://erciapps.sytes.net:11014/productos/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaProductos =
                       JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task CrearProducto()
        {

            await MopupService.Instance.PushAsync(new ProductoFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarProducto()
        {
            var mopup = new ProductoFormularioMopup();
            var vm = new ProductoFormularioViewModel();
            vm.IsEditMode = true;
            vm.ProductoInfo = (ProductoInfo)SelectedProducto.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarProducto()
        {
            if (SelectedProducto != null)
            {                
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://erciapps.sytes.net:11014/productos/borrar/" + SelectedProducto.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
