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
    public partial class GestionProductosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private ProductoInfo selectedProducto;
        [ObservableProperty]
        private bool isDistribuidoresVisible;
        [RelayCommand]
        public async void GetProductos()
        {
            RequestModel request = new RequestModel()
            {
                Method = "GET",
                Route = "http://192.168.20.102:8080/productos/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaProductos =
                       JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
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
        public async Task EditarProducto()
        {
            var mopup = new ProductoFormularioMopup();
            var vm = new ProductoFormularioViewModel();
            vm.ProductoInfo = (ProductoInfo)SelectedProducto.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarProducto()
        {
            if (SelectedProducto != null)
            {                
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://192.168.20.102:8080/productos/borrar/"+SelectedProducto.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
                await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
