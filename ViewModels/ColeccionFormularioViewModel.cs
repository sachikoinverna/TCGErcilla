using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
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
using static System.Net.WebRequestMethods;

namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(ColeccionInfo), "ColeccionInfo")]
    public partial class ColeccionFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;

        [ObservableProperty]
        private string rutaImagen;
        [ObservableProperty]
        private bool isEditMode;
        public ColeccionFormularioViewModel()
        {
            ColeccionInfo = new ColeccionInfo();
        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
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
            if (IsEditMode)
            {
                _coleccion.Id = ColeccionInfo.Id;
            }
            else
            {
                _coleccion.Id = null;
            }
            _coleccion.Nombre = ColeccionInfo.Nombre;
            _coleccion.FechaLanzamiento = ColeccionInfo.FechaLanzamiento;
            var request = new RequestModel()
            {
                Data = _coleccion,
                Method = "POST",
                Route = "http://192.168.20.102:8080/colecciones/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await UploadImage(response.Data.ToString());
            _coleccion.Id = Convert.ToInt32(response.Data);
            string extension = Path.GetExtension(RutaImagen);
            _coleccion.UrlImagen = "https://www.dropbox.com/home/Aplicaciones/TCGErcilla/collections/?preview=" + response.Data.ToString + extension;

            var request2 = new RequestModel()
            {
                Data = _coleccion,
                Method = "POST",
                Route = "http://192.168.20.102:8080/colecciones/crear"
            };
            ResponseModel response2 = await APIService.ExecuteRequest(request2);
            await CerrarMopup();
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
        [RelayCommand]
        public async Task CerrarMopup()
        {
            await MopupService.Instance.PopAllAsync();
        }
    }
}
