using CommunityToolkit.Maui;
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
    [QueryProperty(nameof(IsEditMode), "IsEditMode")]

    public partial class DistribuidorFormularioViewModel : ObservableObject
    {
        [ObservableProperty]
        private DistribuidorInfo distribuidorInfo;
        [ObservableProperty]
        private bool isEditMode;
        [ObservableProperty]
        private string rutaImagen;
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
           if (!IsEditMode)
            {
                DistribuidorInfo = new DistribuidorInfo();
            }
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
                Route = "http://localhost:8080/distribuidores/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await CerrarMopup();
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");


        }
        [RelayCommand]
        public async Task EliminarDistribuidor()
        {
            {
                var request = new RequestModel()
                {
                    Data = DistribuidorInfo.Id,
                    Method = "POST",
                    Route = "http://localhost:8080/distribuidores/eliminar" + DistribuidorInfo.Id

                    //Route = "http://192.168.20.102:8080/distribuidores/eliminar" +DistribuidorInfo.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await CerrarMopup();
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            }
        }
        [RelayCommand]
        public async Task CerrarMopup()
        {
            await MopupService.Instance.PopAllAsync();
            var currentPage = Shell.Current.CurrentPage as ContentPage;
            if (currentPage != null)
            {

                var viewModel = currentPage.BindingContext as GestionDistribuidoresViewModel;
                if (viewModel != null)
                {
                    viewModel.GetDistribuidores();
                    OnPropertyChanged(nameof(viewModel.ListaDistribuidores));

                }

            }
        }
    }
}
