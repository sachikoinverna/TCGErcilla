using CommunityToolkit.Maui;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Utils;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(CartaInfo), "CartaInfo")]
    [QueryProperty(nameof(IsEditMode), "IsEditMode")]

    public partial class CartaFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        public CartaInfo cartaInfo;

        [ObservableProperty]
        private string rutaImagen;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;

        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            
            GetColecciones();
            if (IsEditMode) {
                RutaImagen = CartaInfo.UrlImagen;

            }
            else
            {
                CartaInfo = new CartaInfo();
                ColeccionInfo = new ColeccionInfo();
                RutaImagen = "carta_default.png";

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
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task CrearCarta()
        {
            if (ColeccionInfo != null)
            {
                var _carta = new CartaDto();

                if (IsEditMode)
                {
                    _carta.Id = CartaInfo.Id;
                }
                else
                {
                    _carta.Id = null;

                }
                ColeccionInfo Coleccion = CartaInfo.SelectedColeccion;
                ColeccionDto ColeccionDto = new ColeccionDto();
                ColeccionDto.Id = Coleccion.Id;
                _carta.Nombre = CartaInfo.Nombre;
                _carta.NumeroColeccion = CartaInfo.NumeroColeccion;
                _carta.Coleccion = ColeccionDto;

                var request = new RequestModel()
                {
                    Data = _carta,
                    Method = "POST",
                    Route = "http://localhost:8080/cartas/crear"

                    //                Route = "http://192.168.20.102:8080/cartas/crear"
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                _carta.Id = Convert.ToInt32(response.Data);

                await UploadImage(_carta.Id.ToString());
                _carta.UrlImagen = RutaImagen;
                string extension = Path.GetExtension(RutaImagen);
                _carta.UrlImagen = "http://localhost:8081/dropbox/download/card/" + _carta.Id + extension;

                var request2 = new RequestModel()
                {
                    Data = _carta,
                    Method = "POST",
                    Route = "http://localhost:8080/cartas/crear"

                    //Route = "http://192.168.20.102:8080/cartas/crear"
                };
                ResponseModel response2 = await APIService.ExecuteRequest(request2);
                await CerrarMopup();
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una coleccion", "Aceptar");
            }
        }

        [RelayCommand]
        public async Task<bool> UploadImage(string idCarta)
        {
            try
            {
                await UploadImageService.UploadImageAsync(RutaImagen, idCarta,
                    "http://localhost:8081/dropbox/upload/card");
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
            if (currentPage != null) { 

                var viewModel = currentPage.BindingContext as GestionCartasViewModel;
                if (viewModel != null)
                {
                    viewModel.GetCartas();
                    OnPropertyChanged(nameof(viewModel.ListaCartas));

                }

            }
        }
        [RelayCommand]
        public async Task EliminarCarta()
        {
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://localhost:8080/cartas/borrar/" + CartaInfo.Id

                    //Route = "http://192.168.20.102:8080/cartas/borrar/" + SelectedColeccion.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await CerrarMopup();
                  await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }

    }
}
