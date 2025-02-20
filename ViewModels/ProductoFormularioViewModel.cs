using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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
        public ProductoFormularioViewModel()
        {
            ProductoInfo productoInfo = new ProductoInfo();
            //RutaImagen = "http://localhost:8081/dropbox/download/imagen_bonita.png";
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
    }
}
