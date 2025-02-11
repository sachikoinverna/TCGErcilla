using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using UraniumUI;

namespace TCGErcilla
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                    fonts.AddMaterialSymbolsFonts();
                    fonts.AddFluentIconFonts();
                    fonts.AddFontAwesomeIconFonts();
                }).UseMauiCommunityToolkit().UseUraniumUI().UseUraniumUIBlurs().UseUraniumUIMaterial().UseFFImageLoading().ConfigureMopups();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
