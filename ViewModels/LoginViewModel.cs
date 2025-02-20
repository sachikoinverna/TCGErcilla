using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Utils;

namespace TCGErcilla.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        [RelayCommand]
        public async Task OnNavigated(object _navigator)
        {
            if (_navigator is not WebView webView) return;

            string url = await webView.EvaluateJavaScriptAsync("window.location.href;");
            Debug.WriteLine($"La web actual: {url}");

            if (!url.Contains("/login_ok")) return;

            string cookies = await webView.EvaluateJavaScriptAsync("document.cookie");
            Debug.WriteLine($"Cookies: {cookies}");

            var cookieDict = cookies.Split(';')
                                    .Select(c => c.Trim().Split('='))
                                    .Where(parts => parts.Length == 2)
                                    .ToDictionary(parts => parts[0], parts => parts[1].Replace("\"", "").Replace("\\", ""));

            if (cookieDict.TryGetValue("token", out string? token) &&
                cookieDict.TryGetValue("userlogin", out string? userlogin))
            { 
                    Debug.WriteLine($"Token: {token}");
                    Debug.WriteLine($"UserLogin: {userlogin}");
                    await SecureStorage.SetAsync("auth_token", token);
                    await SecureStorage.SetAsync("user", userlogin);
            }
            JwtPayload payload = JwtUtils.DecodeJwtPayload(token);
            string rol = payload["admin"].ToString();
            Debug.WriteLine("Payload: " + rol);
            await App.Current.MainPage.DisplayAlert("ÉXITO", "Login correcto. El rol es: " + rol, "EMPEZAR");
            if (rol.Equals("true"))
            {
                await MostrarMainPage();
            }

        }
        [RelayCommand]

        public async Task MostrarMainPage()
        {
            try
            {
                await Shell.Current.GoToAsync("MainPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a MainPage: {ex.Message}");
            }

        }
    }
}
