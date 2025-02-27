using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Utils;

namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(ProductoInfo), "ProductoInfo")]
    public partial class ProductoFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private ProductoInfo productoInfo;
        [ObservableProperty]
        private string rutaImagen;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ObservableCollection<DistribuidorInfo> listaDistribuidores = new ObservableCollection<DistribuidorInfo>();
        [ObservableProperty]
        private ObservableCollection<TipoProductoInfo> listaTipoProducto = new ObservableCollection<TipoProductoInfo>();
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;
        [ObservableProperty]
        private DistribuidorInfo distribuidorInfo;
        [ObservableProperty]
        private TipoProductoInfo tipoProductoInfo;
        public ProductoFormularioViewModel()
        {
            ProductoInfo productoInfo = new ProductoInfo();
        }
        [RelayCommand]
        public async void SeleccionarImagen()
        {
            var file = await FileSelector.SelectImageAsync();
            if (file != null)
            {
                RutaImagen = file.FullPath;
            }
        }
        [RelayCommand]
        public async void GetColecciones()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                //Route = "http://localhost:8080/colecciones/todas"
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
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async void GetTiposProducto()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/tipo_producto_todos"
                //Route = "http://192.168.20.102:8080/tipo_producto/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaTipoProducto =
                       JsonConvert.DeserializeObject<ObservableCollection<TipoProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async void GetDistribuidores()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/distribuidores/todos"
                //Route = "http://192.168.20.102:8080/distribuidores/todos"
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
        public void EstablecerValoresIniciales()
        {
            //if (ProductoInfo.Id != null) //Quiere decir que estamos editando
            //{
                //RutaImagen = ProductoInfo.AvatarUrl;
              //  IsEditMode = true;
            //} else {
                IsEditMode = false;
            //}
            //RutaImagen = "producto_default.png";
            GetDistribuidores();
            GetColecciones();
        }
        [RelayCommand]
        public async Task CrearProducto()
        {
            var _producto = new ProductoDto();
            //if (ProductoInfo.Id != null)
            //{
              //  _producto.Id = ProductoInfo.Id;
            //}
           // _producto.Nombre = ProductoInfo.Nombre;
            //_producto.UrlImagen = ProductoInfo.UrlImagen;
            //_producto.FechaLanzamiento = ProductoInfo.FechaLanzamiento;
            var request = new RequestModel()
            {
                Data = _producto,
                Method = "POST",
                Route = "http://localhost:8080/cartas/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await UploadImage(response.Data.ToString());
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
        }
        [RelayCommand]
        public async Task<bool> UploadImage(string idPersona)
        {
            try
            {
                string _rutaImagen = RutaImagen;
                RutaImagen = "loading.gif";
                await UploadImageService.UploadImageAsync(_rutaImagen, idPersona,
                    "http://localhost:8081/dropbox/upload/product");
                await App.Current.MainPage.DisplayAlert("ÉXITO",
                    "Subiendo imagen...",
                    "ACEPTAR");
                RutaImagen = _rutaImagen;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al subir imagen");
                await App.Current.MainPage.DisplayAlert("ERROR",
                    "No se pudo subir la imagen",
                    "ACEPTAR");
                return false;
            }
        }
        [RelayCommand]
        public async Task CerrarMopup()
        {
            await MopupService.Instance.PopAllAsync();
        }
    }
}
