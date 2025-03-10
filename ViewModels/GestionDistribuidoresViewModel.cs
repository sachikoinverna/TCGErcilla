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
        private ObservableCollection<ProductoInfo> listaProductos = new ObservableCollection<ProductoInfo>();
        [ObservableProperty]
        private DistribuidorInfo selectedDistribuidor;
        [ObservableProperty]
        private bool isProductosVisible;
        [ObservableProperty]
        private bool isReportesVisible;
        [ObservableProperty]
        private bool isDistribuidoresVisible;
        [ObservableProperty]
        private string urlPDF;
        [ObservableProperty]
        private bool isImagenVisible;

        [ObservableProperty]
        private string filtroNombre; 

        [RelayCommand]
        public void GetPDF()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportDistribuidoresByNombre/" + FiltroNombre;
        }

        [RelayCommand]
        public void EstadoInicial()
        {
            IsProductosVisible = false;
            IsReportesVisible = false;
            IsImagenVisible = true;

            IsDistribuidoresVisible = false;
        }
        [RelayCommand]
        public async Task MostrarProductos()
        {
            if (SelectedDistribuidor != null)
            {
                RequestModel request = new RequestModel()
                {
                    Method = "GET",
                    Route = "http://erciapps.sytes.net:11014/productos/buscar_distribuidor/" + SelectedDistribuidor.Id
                };

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success.Equals(0))
                {
                    try
                    {
                        ListaProductos =
                           JsonConvert.DeserializeObject<ObservableCollection<ProductoInfo>>(response.Data.ToString());
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                    }
                }
                IsProductosVisible = true;
                IsReportesVisible = false;
            } else {


                    App.Current.MainPage.DisplayAlert("Atencion", "Debes seleccionar un producto", "Aceptar");
                    return;
                
            }
        }
        [RelayCommand]
        public void MostrarDistribuidores()
        {
                IsDistribuidoresVisible = true;
                IsProductosVisible = false;
                IsReportesVisible = false;
            IsImagenVisible = false;


        }
        [RelayCommand]
        public void OcultarDistribuidores()
        {
            IsDistribuidoresVisible = false;
            IsProductosVisible = false;
            IsReportesVisible = false;
            IsImagenVisible = true;


        }
        [RelayCommand]
        public void OcultarProductos()
        {
            IsProductosVisible = false;
            IsReportesVisible = false;
            FiltroNombre = null;

        }
        [RelayCommand]
        public void MostrarReportes()
        {
            UrlPDF = "http://erciapps.sytes.net:11015/report/getReportDistribuidoresAll";
            IsDistribuidoresVisible = false;

            IsProductosVisible = false;
            IsReportesVisible = true;
            IsImagenVisible = false;

        }
        [RelayCommand]
        public void OcultarReportes()
        {
            IsDistribuidoresVisible = false;
            FiltroNombre = null;
            IsProductosVisible = false;
            IsReportesVisible = false;
            IsImagenVisible = true;

        }
        [RelayCommand]
        public async void GetDistribuidores()
        {
            RequestModel request = new RequestModel()
            {
               Method = "GET",
               Route = "http://erciapps.sytes.net:11014/distribuidores/todos"
            };

            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success.Equals(0))
            {
                try
                {
                    ListaDistribuidores =
                       JsonConvert.DeserializeObject<ObservableCollection<DistribuidorInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Aceptar");
                }
            }
        }
        [RelayCommand]
        public async Task AbrirMopupDistribuidor()
        {
            var mopup = new DistribuidorFormularioMopup();
            var vm = new DistribuidorFormularioViewModel();
            vm.IsEditMode = false;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
        [RelayCommand]
        public async Task EditarDistribuidor()
        {
            var mopup = new DistribuidorFormularioMopup();
            var vm = new DistribuidorFormularioViewModel();
            vm.DistribuidorInfo = (DistribuidorInfo)SelectedDistribuidor.Clone();
            vm.IsEditMode = true;
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
    }
}
