using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;
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
           // if (ProductoInfo.Id != null) //Quiere decir que estamos editando
           //{
           //   RutaImagen = ProductoInfo.AvatarUrl;
           // }
            RutaImagen = "producto_default.png";
        }
        [RelayCommand]
        public async void Submit()
        {

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
