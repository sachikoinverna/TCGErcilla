using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
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

namespace TCGErcilla.ViewModels
{
    public partial class GestionTiposProductoViewModel : ObservableObject 
    {
        [ObservableProperty]
        private ObservableCollection<TipoProductoInfo> listaTiposProducto = new ObservableCollection<TipoProductoInfo>();
        [ObservableProperty]
        private TipoProductoInfo selectedTipoProductoInfo;

        [RelayCommand]
        public async void GetTiposProducto()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Data = string.Empty,
                Route = "http://192.168.20.102:8080/tipo_producto/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaTiposProducto.Add(new TipoProductoInfo(1,"ETB"));
                    ListaTiposProducto =
                       JsonConvert.DeserializeObject<ObservableCollection<TipoProductoInfo>>(response.Data.ToString());
                }
                catch (Exception ex) { }
            }
        }
        [RelayCommand]
        public async Task CrearTipoProducto()
        {

            await MopupService.Instance.PushAsync(new TipoProductoFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarTipoProducto()
        {
            var mopup = new TipoProductoFormularioMopup();
            var vm = new TipoProductoFormularioViewModel();
            vm.TipoProductoInfo = (TipoProductoInfo)SelectedTipoProductoInfo.Clone();
            vm.IsEditMode = true;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarTiposProducto()
        {
            if (SelectedTipoProductoInfo != null)
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://192.168.20.102:8080/tipo_producto/borrar/" + SelectedTipoProductoInfo.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
