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
    [QueryProperty(nameof(TipoProductoInfo), "TipoProductoInfo")]
    [QueryProperty(nameof(IsEditMode), "IsEditMode")]

    public partial class TipoProductoFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private TipoProductoInfo tipoProductoInfo;
        [ObservableProperty]
        private bool isEditMode;
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
            if (!IsEditMode)
            {
                TipoProductoInfo = new TipoProductoInfo();
            }
        }
        [RelayCommand]
        public async Task CrearTipoProducto()
        {
            var tipo_producto = new TipoProductoDto();
            if (IsEditMode)
            {
                tipo_producto.Id = TipoProductoInfo.Id;
            }
            else
            {
                tipo_producto.Id = null;
            }
            tipo_producto.Tipo = TipoProductoInfo.Tipo;
            var request = new RequestModel()
            {
                Data = tipo_producto,
                Method = "POST",
                Route = "http://erciapps.sytes.net:11014/tipo_producto/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            if (IsEditMode)
            {
                await App.Current.MainPage.DisplayAlert("Mensaje", "Tipo producto editado", "ACEPTAR");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "ACEPTAR");
            }
               
            await CerrarMopup();
        }
        [RelayCommand]
        public async Task CerrarMopup()
        {
            await MopupService.Instance.PopAllAsync();
            var currentPage = Shell.Current.CurrentPage as ContentPage;
            if (currentPage != null)
            {

                var viewModel = currentPage.BindingContext as GestionTiposProductoViewModel;
                if (viewModel != null)
                {
                    viewModel.GetTiposProducto();
                    OnPropertyChanged(nameof(viewModel.ListaTiposProducto));

                }

            }

        }
        [RelayCommand]
        public async Task EliminarTipoProducto()
        {
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://erciapps.sytes.net:11014/tipo_producto/borrar/" + TipoProductoInfo.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await CerrarMopup();
                  await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");


            }
        }
    }
}
