﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    //[QueryProperty(nameof(CartaInfo), "CartaInfo")]

    public partial class CartaFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private CartaInfo cartaInfo;

        [ObservableProperty]
        private string rutaImagen;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ColeccionInfo coleccionInfo;

        public CartaFormularioViewModel()
        {
            CartaInfo = new CartaInfo();
            RutaImagen = "carta_default.png";
        }

        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
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
        public async void GetColecciones()
        {

            RequestModel request = new RequestModel()
            {
                Method = "GET",
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
        public async Task CrearCarta()
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
            _carta.Nombre = CartaInfo.Nombre;
            _carta.UrlImagen = CartaInfo.UrlImagen;
            var request = new RequestModel()
            {
                Data = _carta,
                Method = "POST",
                Route = "http://localhost:8080/cartas/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
         //   await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
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
