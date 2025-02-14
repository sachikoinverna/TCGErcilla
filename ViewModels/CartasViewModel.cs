using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;
using TCGErcilla.Views.Mopups;

namespace TCGErcilla.ViewModels
{
    public partial class CartasViewModel:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CartaInfo> listaCartas = new ObservableCollection<CartaInfo>();
        [ObservableProperty]
        private CartaInfo selectedCartaInfo;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private int numCarta;
        [RelayCommand]
        public async void GetCartas()
        {
            for (int i = 0; i < 5; i++)
            {
                listaCartas.Add(new CartaInfo(i, "a", i));
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
        public async Task AbrirMopupCarta()
        {
            await MopupService.Instance.PushAsync(new DistribuidorFormularioMopup());
        }
        [RelayCommand]
        public async Task EditarCarta()
        {
            var mopup = new CartaFormularioMopup();
            var vm = new CartaFormularioViewModel();
            vm.CartaInfo = (CartaInfo)SelectedCartaInfo.Clone();
            mopup.BindingContext = vm;
            await MopupService.Instance.PushAsync(mopup);
        }
    }
}
