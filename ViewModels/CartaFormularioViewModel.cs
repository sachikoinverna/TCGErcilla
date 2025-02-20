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
    //[QueryProperty(nameof(CartaInfo), "CartaInfo")]

    public partial class CartaFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private CartaInfo cartaInfo;

        [ObservableProperty]
        private string rutaImagen;

        public CartaFormularioViewModel()
        {
            CartaInfo = new CartaInfo();
            RutaImagen = "carta_default.png";
        }
        [RelayCommand]
        public async void Submit()
        {

        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            if (CartaInfo.Id != null) //Quiere decir que estamos editando
            {

                RutaImagen = CartaInfo.UrlImagen;

            }
            RutaImagen = "carta_default.png";
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
        public async Task CrearCarta()
        {
            var _carta = new CartaDto();
            if (CartaInfo.Id != null) {
                _carta.Id = CartaInfo.Id;
            }
            _carta.Nombre = CartaInfo.Nombre;
            _carta.UrlImagen = CartaInfo.UrlImagen;
            var request = new RequestModel()
            {
                Data = _carta,
                Method = "POST",
                Route = "http://localhost:8080/cartas/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
        }

        [RelayCommand]
        public async Task<bool> UploadImage(string idCarta)
        {
            try
            {
                string _rutaImagen = RutaImagen;
               // RutaImagen = "loading.gif";
                await UploadImageService.UploadImageAsync(_rutaImagen, idCarta,
                    "http://localhost:8081/dropbox/upload/card");
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
