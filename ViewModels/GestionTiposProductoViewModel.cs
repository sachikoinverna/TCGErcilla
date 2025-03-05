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
    public partial class GestionTiposProductoViewModel : ObservableObject 
    {
        [ObservableProperty]
        private ObservableCollection<TipoProductoInfo> listaTiposProducto = new ObservableCollection<TipoProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private TipoProductoInfo selectedTipoProductoInfo;

        [ObservableProperty]
        private bool isProductosVisible;
        [ObservableProperty]
        private bool isTipoProductosVisible;
        [ObservableProperty]
        private bool isReportesVisible;
        [ObservableProperty]
        private bool isImagenVisible;
        [ObservableProperty]
        private string urlPDF; // Para source del webview (endpoint)

        [ObservableProperty]
        private string filtroNombre; // Capturar el texto del correo

        [RelayCommand]
        public void GetPDF()
        {
            UrlPDF = "http://localhost:8082/report/getReportTiposProductoByNombre/" + FiltroNombre;
        }


        [RelayCommand]
        public void EstadoInicial()
        {
            IsTipoProductosVisible = false;
            IsProductosVisible = false;
            IsReportesVisible = false;
            IsImagenVisible = true;

        }
        [RelayCommand]
        public async Task MostrarProductos()
        {
            if (SelectedTipoProductoInfo == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un tipo de producto", "Aceptar");
                return;
            }
            else
            {
                if (SelectedTipoProductoInfo.Id != 0)
                {
                    IsProductosVisible = true;
                    IsReportesVisible = false;
                    IsImagenVisible = false;

                    IsTipoProductosVisible = true;
                    RequestModel request = new RequestModel()
                    {
                        Method = "GET",
                        Route = "http://localhost:8080/productos/buscar/tipo_producto/" + SelectedTipoProductoInfo.Id

                        // Route = "http://192.168.20.102:8080/productos/buscar/tipo_producto/" + SelectedTipoProductoInfo.Id
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
        public async Task MostrarInforme()
        {
            UrlPDF = "http://localhost:8082/report/getReportTiposProductoAll";
                IsProductosVisible = false;
                IsReportesVisible = true;
            IsTipoProductosVisible = false;
            IsImagenVisible = false;


        }
        [RelayCommand]
        private void OcultarTipoProducto()
        {
            IsTipoProductosVisible = false;
            IsImagenVisible = true;

        }
        [RelayCommand]
        public async Task MostrarTipoProducto()
        {
            GetTiposProducto();            IsProductosVisible = false;
            IsReportesVisible = false;
            IsTipoProductosVisible = true;
            IsImagenVisible = false;


        }
        [RelayCommand]
        private void OcultarProductos()
        {
            IsProductosVisible = false;
        }
        [RelayCommand]
        private void OcultarInforme()
        {
            IsReportesVisible = false;
            IsImagenVisible = true;


        }
        [RelayCommand]
            public async void GetTiposProducto()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/tipo_producto/todos"
                //Route = "http://192.168.20.102:8080/tipo_producto/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaTiposProducto.Add(new TipoProductoInfo(1,"ETB"));
                    ListaTiposProducto =
                       JsonConvert.DeserializeObject<ObservableCollection<TipoProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async Task CrearTipoProducto()
        {
            var mopup = new TipoProductoFormularioMopup();
            var vm = new TipoProductoFormularioViewModel();
            vm.IsEditMode = false;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EditarTipoProducto()
        {
            var mopup = new TipoProductoFormularioMopup();
            var vm = new TipoProductoFormularioViewModel();
            vm.TipoProductoInfo = (TipoProductoInfo)SelectedTipoProductoInfo.Clone();
            vm.IsEditMode = true;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
    }
}
