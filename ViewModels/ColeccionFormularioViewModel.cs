using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Utils;
using TCGErcilla.Views;
using static System.Net.WebRequestMethods;


namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(ColeccionInfo), "ColeccionInfo")]
    [QueryProperty(nameof(IsEditMode), "IsEditMode")]

    public partial class ColeccionFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;

        [ObservableProperty]
        private string rutaImagen;
        [ObservableProperty]
        private bool isEditMode;
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            if (IsEditMode)
            {
                RutaImagen = ColeccionInfo.UrlImagen;

            }
            else
            {
                ColeccionInfo = new ColeccionInfo();
                ColeccionInfo.FechaLanzamiento = DateTime.Now;
                RutaImagen = "collection_default.png";
                OnPropertyChanged(nameof(ColeccionInfo));
                OnPropertyChanged(nameof(ColeccionInfo.FechaLanzamiento));
            }
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
            _coleccion.NumeroCartas = ColeccionInfo.NumeroCartas;
            _coleccion.FechaLanzamiento = ColeccionInfo.FechaLanzamiento.Date;
            var request = new RequestModel()
            {
                Data = _coleccion,
                Method = "POST",
                Route = "http://erciapps.sytes.net:11014/colecciones/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await UploadImage(response.Data.ToString());

            _coleccion.Id = Convert.ToInt32(response.Data);

            string extension = Path.GetExtension(RutaImagen);
            _coleccion.UrlImagen = "http://erciapps.sytes.net:11016/dropbox/download/collection/" + _coleccion.Id + extension;

            var request2 = new RequestModel()
            {
                Data = _coleccion,
                Method = "POST",
                Route = "http://erciapps.sytes.net:11014/colecciones/crear"
               

            };
            ResponseModel response2 = await APIService.ExecuteRequest(request2);
            await CerrarMopup();
            if (IsEditMode)
            {
                await App.Current.MainPage.DisplayAlert("Mensaje", "Coleccion editada", "Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
            
        }
        [RelayCommand]
        public async Task<bool> UploadImage(string idPersona)
        {
            try
            {
                await UploadImageService.UploadImageAsync(RutaImagen, idPersona,
                    "http://erciapps.sytes.net:11016/dropbox/upload/collection");
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
            var mainPage = Application.Current.MainPage as NavigationPage;
            var currentPage = Shell.Current.CurrentPage as ContentPage;
            if (currentPage != null)
            {
                var viewModel = currentPage.BindingContext as GestionColeccionesViewModel;
                if (viewModel != null)
                {
                    viewModel.GetColecciones();
                    OnPropertyChanged(nameof(viewModel.ListaColecciones));

                }
            }
        }
        [RelayCommand]
        public async Task EliminarColeccion()
        {
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://erciapps.sytes.net:11014/colecciones/borrar/" + ColeccionInfo.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                CerrarMopup();
                 await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            }
        }
    }
}
