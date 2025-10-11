//
// Vijay Anand E G - https://egvijayanand.in/
//
// .NET 10 File-based Apps
// WinForms Blazor Hybrid Sample
#:sdk Microsoft.NET.Sdk.Razor

#:property TargetFramework=net10.0-windows7.0
#:property UseWindowsForms=true
#:property OutputType=WinExe
#:property PublishAot=false
#:property RootNamespace=HybridApp

#:package Microsoft.AspNetCore.Components.WebView.WindowsForms@10.*-*

#:project ..\HybridLib\HybridLib.csproj

using HybridLib.Data;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace HybridApp;

static class Program
{
    // Explicitly define the Main method to configure this attribute.
    [STAThread]
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();
        services.AddSingleton<WeatherForecastService>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif

        Application.Run(new Form()
        {
            Text = "HybridApp",
            WindowState = FormWindowState.Maximized,
            Controls =
            {
                new BlazorWebView()
                {
                    Dock = DockStyle.Fill,
                    HostPage = "wwwroot/index.html",
                    Services = services.BuildServiceProvider(),
                    StartPath = "/counter",
                    RootComponents =
                    {
                        new RootComponent("#app", typeof(Main), null)
                    }
                }
            }
        });
    }
}
