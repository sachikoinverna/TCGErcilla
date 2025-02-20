using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;
using TCGErcilla.Models;
using TCGErcilla.Services;
using TCGErcilla.Views.Mopups;
using Mopups.Services;

namespace TCGErcilla.ViewModels
{
    public partial class GestionDistribuidoresViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DistribuidorInfo> listaDistribuidores = new ObservableCollection<DistribuidorInfo>();
        [ObservableProperty]
        private DistribuidorInfo selectedDistribuidor;

        [RelayCommand]
        public async void GetDistribuidores()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://localhost:8080/distribuidores/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaDistribuidores =
                       JsonConvert.DeserializeObject<ObservableCollection<DistribuidorInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task AbrirMopupDistribuidor()
        {

            await MopupService.Instance.PushAsync(new DistribuidorFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarDistribuidor()
        {
            var mopup = new DistribuidorFormularioMopup();
            var vm = new DistribuidorFormularioViewModel();
            vm.DistribuidorInfo = (DistribuidorInfo)SelectedDistribuidor.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarDistribuidor()
        {
            if (SelectedDistribuidor != null)
            {
                //var _producto = new ProductoDto();
                //if (ProductoInfo.Id != null)
                //{
                // _producto.Id = ProductoInfo.Id;
                //_producto.Nombre = ProductoInfo.Nombre;
                //_producto.UrlImagen = ProductoInfo.UrlImagen;
                //_producto.FechaLanzamiento = ProductoInfo.FechaLanzamiento;
                var request = new RequestModel()
                {
                    Data = SelectedDistribuidor.Id,
                    Method = "POST",
                    Route = "http://localhost:8080/cartas/crear"
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
