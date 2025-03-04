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
using System.Xml.Linq;
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Utils;

namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(ProductoInfo), "ProductoInfo")]
    [QueryProperty(nameof(IsEditMode), "IsEditMode")]

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
        public async Task GetColecciones()
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
        public async Task GetTiposProducto()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://localhost:8080/tipo_producto/todos"
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
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async Task GetDistribuidores()
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
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async Task EstablecerValoresIniciales()
        {
           await GetTiposProducto();
          await  GetDistribuidores();
           await GetColecciones();
            if (IsEditMode)
            {
                int id = ProductoInfo.Distribuidores[0].Id;
                foreach(var distribuidor in ListaDistribuidores)
                {
                    if (distribuidor.Id == id)
                    {
                        ProductoInfo.SelectedDistribuidor = distribuidor;
                        OnPropertyChanged(nameof(ProductoInfo));
                        OnPropertyChanged(nameof(ProductoInfo.SelectedDistribuidor));
                        break;
                    }
                }

                RutaImagen = ProductoInfo.UrlImagen;

            }
            else
            {
                ProductoInfo = new ProductoInfo();
                RutaImagen = "product_default.png";
                ProductoInfo.UrlImagen = RutaImagen;
            }
        }
        [RelayCommand]
        public async Task CrearProducto()
        {
            if (ProductoInfo.SelectedColeccion != null && ProductoInfo.SelectedTipoProducto != null && ProductoInfo.SelectedColeccion != null)
        
                {
                var _producto = new ProductoDto();

                if (IsEditMode)
                {
                    _producto.Id = ProductoInfo.Id;
                }
                else
                {
                    _producto.Id = null;

                }
                ColeccionInfo Coleccion = ProductoInfo.SelectedColeccion;
                TipoProductoInfo TipoProducto = ProductoInfo.SelectedTipoProducto;
                DistribuidorInfo Distribuidor = ProductoInfo.SelectedDistribuidor;
                ColeccionDto ColeccionDto = new ColeccionDto();
                ColeccionDto.Id = Coleccion.Id;
                TipoProductoDto TipoProductoDto = new TipoProductoDto();
                TipoProductoDto.Id = TipoProducto.Id;
                DistribuidorDto DistribuidorDto = new DistribuidorDto();
                DistribuidorDto.Id = Distribuidor.Id;
                ObservableCollection<DistribuidorDto> ListaDistribuidores = new ObservableCollection<DistribuidorDto>();
                ListaDistribuidores.Add(DistribuidorDto);

                _producto.Nombre = ProductoInfo.Nombre;
                _producto.TipoProducto = TipoProductoDto;
                _producto.Coleccion = ColeccionDto;
                _producto.Distribuidores = ListaDistribuidores;
                var request = new RequestModel()
                {
                    Data = _producto,
                    Method = "POST",
                    Route = "http://localhost:8080/productos/crear"
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await UploadImage(response.Data.ToString());
                _producto.Id = Convert.ToInt32(response.Data);
                string extension = Path.GetExtension(RutaImagen);
            
                _producto.UrlImagen = "http://localhost:8081/dropbox/download/product/" + _producto.Id + extension;

                var request2 = new RequestModel()
                {
                    Data = _producto,
                    Method = "POST",
                    Route = "http://localhost:8080/productos/crear"

                    //  Route = "http://192.168.20.102:8080/productos/crear"
                };
                ResponseModel response2 = await APIService.ExecuteRequest(request2);
                await CerrarMopup();
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            }
        }
        [RelayCommand]
        public async Task<bool> UploadImage(string idPersona)
        {
            try
            {
                await UploadImageService.UploadImageAsync(RutaImagen, idPersona,
                    "http://localhost:8081/dropbox/upload/product");
                await App.Current.MainPage.DisplayAlert("ÉXITO",
                    "Subiendo imagen...",
                    "ACEPTAR");
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
            var currentPage = Shell.Current.CurrentPage as ContentPage;
            if (currentPage != null)
            {

                var viewModel = currentPage.BindingContext as GestionProductosViewModel;
                if (viewModel != null)
                {
                    viewModel.GetProductos();
                    OnPropertyChanged(nameof(viewModel.ListaProductos));

                }
            }

        }
        [RelayCommand]
        public async Task EliminarProducto()
        {
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://localhost:8080/productos/borrar/" + ProductoInfo.Id

                    //Route = "http://192.168.20.102:8080/productos/borrar/" + SelectedColeccion.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await CerrarMopup();
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");


            }
        }
    }
}
