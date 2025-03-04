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
        private ObservableCollection<TipoProductoInfo> listaTipoInfo = new ObservableCollection<TipoProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<DistribuidorInfo> listaDistribuidores = new ObservableCollection<DistribuidorInfo>();
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ProductoInfo selectedProducto;
        [ObservableProperty]
        private bool isDistribuidoresVisible;
        [ObservableProperty]
        private bool isReportesVisible;
        [ObservableProperty]
        private bool isProductosVisible;
        [ObservableProperty]
        private string urlPDF; 

        [ObservableProperty]
        private string filtroNombre; 
        [ObservableProperty]
        private TipoProductoInfo filtroTipoProducto;
        [ObservableProperty]
        private DistribuidorInfo filtroDistribuidor;
        [ObservableProperty]
        private ColeccionInfo filtroColeccion;

        [RelayCommand]
        public void GetPDFNombre()
        {
            UrlPDF = "http://localhost:8082/report/getReportProductosByNombre/" + FiltroNombre;
        }

        [RelayCommand]
        public void GetPDFTipoProducto()
        {
            if (FiltroTipoProducto != null)
            {
                UrlPDF = "http://localhost:8082/report/getReportProductosByIdTipoProducto/" + FiltroTipoProducto.Id;
            }

                else
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un tipo de producto", "Aceptar");
                    return;
                
            }
        }
        [RelayCommand]
        public void GetPDFDistribuidor()
        {

            if (FiltroDistribuidor != null)
            {
                UrlPDF = "http://localhost:8082/report/getReportProductosByIdDistribuidor/" + FiltroDistribuidor.Id;
            }
                else
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un distribuidor", "Aceptar");
                    return;
                
            }
        }
        [RelayCommand]
        public void GetPDFColeccion()
        {
            if (FiltroDistribuidor != null)
            {
                UrlPDF = "http://localhost:8082/report/getReportProductosByIdColeccion/" + FiltroColeccion.Id;
            }

                else
                {
                    App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");
                    return;
                
            }
        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            IsProductosVisible = true;
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public void MostrarReportes()
        {
            UrlPDF = "http://localhost:8082/report/getReportProductosAll";
            GetListaTipoProducto();
            GetListaDistribuidores();
            GetListaColecciones();
            IsReportesVisible = true;
            IsProductosVisible = false;

            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public void OcultarReportes()
        {
            IsReportesVisible = false;
            IsProductosVisible = false;

            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public void MostrarDistribuidores()
        {
            if (SelectedProducto != null)
            {
                IsReportesVisible = false;
                IsProductosVisible = true;

                IsDistribuidoresVisible = true;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un distribuidor", "Aceptar");

            }
        }
        [RelayCommand]
        public void OcultarDistribuidores()
        {
            IsReportesVisible = false;
            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public void MostrarProductos()
        {
            GetProductos();
          
                IsReportesVisible = false;
                IsProductosVisible = true;

                IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public void OcultarProductos()
        {
            IsReportesVisible = false;
            IsProductosVisible = false;

            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public async void GetProductos() 
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/productos/todos"
                //Route = "http://192.168.20.102:8080/productos/todos"
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
        [RelayCommand]
        public async void GetListaTipoProducto()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/tipo_producto/todos"
                //Route = "http://192.168.20.102:8080/productos/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaTipoInfo =
                       JsonConvert.DeserializeObject<ObservableCollection<TipoProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async void GetListaDistribuidores()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/distribuidores/todos"
                //Route = "http://192.168.20.102:8080/productos/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaDistribuidores =
                       JsonConvert.DeserializeObject<ObservableCollection<DistribuidorInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async void GetListaColecciones()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/colecciones/todas"
                //Route = "http://192.168.20.102:8080/productos/todos"
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
        public async Task CrearProducto()
        {
            var mopup = new ProductoFormularioMopup();
            var vm = new ProductoFormularioViewModel();
            vm.IsEditMode = false;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EditarProducto()
        {
            var mopup = new ProductoFormularioMopup();
            var vm = new ProductoFormularioViewModel();
            vm.ProductoInfo = (ProductoInfo)SelectedProducto.Clone();
            vm.IsEditMode = true;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
    }
}
