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
    [QueryProperty(nameof(ColeccionInfo), "ColeccionInfo")]
    public partial class ColeccionFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;

        [ObservableProperty]
        private string rutaImagen;
        public ColeccionFormularioViewModel()
        {
            ColeccionInfo = new ColeccionInfo();
        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            //if (ColeccionInfo.Id != null) //Quiere decir que estamos editando
            //{
            // RutaImagen = ColeccionInfo.Url;
            // }
            RutaImagen = "coleccion_default.png";
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
        public async Task CrearColeccion()
        {
            var _coleccion = new ColeccionDto();
            if (ColeccionInfo.Id != null)
            {
                _coleccion.Id = ColeccionInfo.Id;
            }
            _coleccion.Nombre = ColeccionInfo.Nombre;
            _coleccion.FechaLanzamiento = ColeccionInfo.FechaLanzamiento;
            _coleccion.UrlImagen = ColeccionInfo.UrlImagen;
            var request = new RequestModel()
            {
                Data = _coleccion,
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
                    "http://localhost:8081/dropbox/upload/collection");
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
