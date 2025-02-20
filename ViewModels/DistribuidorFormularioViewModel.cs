using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Dto;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;

namespace TCGErcilla.ViewModels
{
    [QueryProperty(nameof(DistribuidorInfo), "DistribuidorInfo")]

    public partial class DistribuidorFormularioViewModel : ObservableObject
    {
        [ObservableProperty]
        private DistribuidorInfo distribuidorInfo;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private string rutaImagen;
        public DistribuidorFormularioViewModel()
        {
            DistribuidorInfo = new DistribuidorInfo();
            //RutaImagen = "http://localhost:8081/dropbox/download/imagen_bonita.png";
        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            RutaImagen = "coleccion_default.png";
        }
        [RelayCommand]
        public async Task CrearDistribuidor()
        {
            var _distribuidor = new DistribuidorDto();
            if(IsEditMode)
            {
                _distribuidor.Id = DistribuidorInfo.Id;
            }
            else
            {
                _distribuidor.Id = null;
            }
            _distribuidor.Nombre = DistribuidorInfo.Nombre;

            var request = new RequestModel()
            {
                Data = _distribuidor,
                Method = "POST",
                Route = "http://192.168.20.102:8080/distribuidores/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            // 🔹 2. Obtener la ruta actual y recargarla
            var currentRoute = Shell.Current.CurrentState.Location.OriginalString;

            // 🔹 3. Volver a la misma página para forzar la recarga
            await Shell.Current.GoToAsync("//"+currentRoute, true);
            await MopupService.Instance.PopAsync();

        }
        [RelayCommand]
        public async Task EliminarDistribuidor()
        {
            if ( DistribuidorInfo.Id != null)
            {
                var request = new RequestModel()
                {
                    Data = DistribuidorInfo.Id,
                    Method = "POST",
                    Route = "http://192.168.20.102:8080/distribuidores/eliminar"
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }

    }
}
