using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [RelayCommand]
        public void CrearDistribuidor()
        {
            // RequestModel request = new RequestModel()
            //{
            //   Method = "POST",
            //  Route = "http://localhost:8080/gastos/crear",
            // Data = vm.SelectedGasto
            //};

            //ResponseModel response = await APIService.ExecuteRequest(request);
            //if (response.Success.Equals(0))
            //{
            //   await App.Current.MainPage.DisplayAlert("PersonasApp",
            //      response.Message,
            //     "ACEPTAR");
        }


    }
}
