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
    public partial class GestionColeccionesViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ColeccionInfo> listaColecciones = new ObservableCollection<ColeccionInfo>();
        [ObservableProperty]
        private ColeccionInfo selectedColeccion;
        [ObservableProperty]
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private ObservableCollection<CartaInfo> listaCartas = new ObservableCollection<CartaInfo>();
        [ObservableProperty]
        private bool isCartasVisible;
        [ObservableProperty]
        private bool isProductosVisible;
        [RelayCommand]
        public void MostrarCartas()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            //if (SelectedColeccion .Gastos.Count > 0)
            //{
             //   IsProductosVisible = false;
              //  IsCartasVisible = true;
            //}
            else
            {
            }
        }
        [RelayCommand]
        public void EstadoInicial()
        {
            IsProductosVisible = false;
            IsCartasVisible = false;
        }
        [RelayCommand]
        private void OcultarProductos()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            // if (SelectedColeccion .Gastos.Count > 0)
            // {
            //    IsCartasVisible = false;
            //   IsProductosVisible = true;
            //}
            else
            {
            }
        }
        [RelayCommand]
        private void OcultarCartas()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            // if (SelectedColeccion .Gastos.Count > 0)
            // {
            //    IsCartasVisible = false;
            //   IsProductosVisible = true;
            //}
            else
            {
            }
        }
        [RelayCommand]
        public void MostrarProductos()
        {
            if (SelectedColeccion == null)
            {
                App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar una persona", "Aceptar");
                return;
            }
            // if (SelectedColeccion .Gastos.Count > 0)
            // {
            //    IsCartasVisible = false;
            //   IsProductosVisible = true;
            //}
            //RequestModel request = new RequestModel()
            //{
               // Method = "GET",
              //  Route = "http://192.168.20.102:8080/colecciones/todas"
            //};

           // ResponseModel response = await APIService.ExecuteRequest(request);
            //if (response.Success.Equals(0))
            ///{
               // try
                //{
                 //   ListaProductos =
                  //     JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
                //}
                //catch (Exception ex)
                //{

                //}
            //}
            else
            {
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
                catch (Exception ex)
                {

                }
            }
        }
        [RelayCommand]
        public async Task CrearColeccion()
        {
            await MopupService.Instance.PushAsync(new ColeccionFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarColeccion()
        {
            var mopup = new ColeccionFormularioMopup();
            var vm = new ColeccionFormularioViewModel();
           // vm.ColeccionInfo = (ColeccionInfo)SelectedColeccion.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EliminarColeccion()
        {
            if (SelectedColeccion != null)
            {
                var request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://192.168.20.102:8080/colecciones/borrar/" + SelectedColeccion.Id
                };
                ResponseModel response = await APIService.ExecuteRequest(request);
              //  await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");
            }
        }
    }
}
