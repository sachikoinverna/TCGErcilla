using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        public TipoProductoFormularioViewModel()
        {
            TipoProductoInfo = new TipoProductoInfo();
            //RutaImagen = "http://localhost:8081/dropbox/download/imagen_bonita.png";
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
            if (TipoProductoInfo.Id != null)
            {
                tipo_producto.Id = TipoProductoInfo.Id;
                }         
                tipo_producto.Tipo = TipoProductoInfo.Tipo;
            var request = new RequestModel()
            {
                Data = tipo_producto,
                Method = "POST",
                Route = "http://localhost:8080/tipo_producto/crear"
            };
            ResponseModel response = await APIService.ExecuteRequest(request);
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "ACEPTAR");
        }
    }
}
