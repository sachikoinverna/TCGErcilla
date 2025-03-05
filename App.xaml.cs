using TCGErcilla.Views;

namespace TCGErcilla
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 1300;
            const int newHeight = 900;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }

    }
}