﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private DistribuidorInfo selectedDistribuidorInfo;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        
        [RelayCommand]
        public async void GetDistribuidores()
        {
            for (int i = 0; i < 5; i++)
            {
                listaDistribuidores.Add(new DistribuidorInfo(i, "a"));
            }

            // RequestModel request = new RequestModel()
            //{
            //   Method = "GET",
            //  Data = string.Empty,
            // Route = "http://localhost:8080/personas/todos"
            //};

            //ResponseModel response = await APIService.ExecuteRequest(request);
            // if (response.Success.Equals(0))
            //{
            //  try
            //{
            //    vm.ListaPersonas =
            //       JsonConvert.DeserializeObject<ObservableCollection<Persona>>(response.Data.ToString());
            // }
            //catch (Exception ex) { }
        }
        [RelayCommand]
        public async Task AbrirMopupDistribuidor()
        {
            await MopupService.Instance.PushAsync(new DistribuidorFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarDistribuidor()
        {
            await Shell.Current.GoToAsync("DistribuidorFormularioView",
                new Dictionary<string, object>()
                {
                    ["DistribuidorInfo"] = SelectedDistribuidorInfo
                });
        }
    }
}
