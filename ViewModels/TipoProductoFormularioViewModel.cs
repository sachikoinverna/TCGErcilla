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

    public partial class TipoProductoFormularioViewModel: ObservableObject
    {
        [ObservableProperty]
        private TipoProductoInfo tipoProductoInfo;
        [ObservableProperty]
        private bool isEditMode;
        public TipoProductoFormularioViewModel()
        {
            TipoProductoInfo = new TipoProductoInfo();
        }
        [RelayCommand]
        public void EstablecerValoresIniciales()
        {
           // if (TipoProductoInfo.Id != null) //Quiere decir que estamos editando
            //{
           //
            //}
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
                Route = "http://192.168.20.102:8080/tipo_producto/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "ACEPTAR");
            CerrarMopup();
        }
        [RelayCommand]
        public async Task CerrarMopup()
        {
            await MopupService.Instance.PopAllAsync();
        }
    }
}
