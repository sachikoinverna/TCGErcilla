using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty]
        private String urlWeb;
        [RelayCommand]
        public void GetLoginWeb()
        {
            UrlWeb = "http://localhost:8080/"; 
        }
    }
}
