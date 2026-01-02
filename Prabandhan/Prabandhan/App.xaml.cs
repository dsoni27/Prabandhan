using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http; // Add this using directive
using Parbandhan.Interfaces;
using Parbandhan.Services;
using Parbandhan.ViewModels;
using Parbandhan.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parbandhan.Models;

namespace Parbandhan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var services = new ServiceCollection();

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
                services.AddSingleton(ServiceProvider => ServiceProvider.GetRequiredService<IOptions<ApiSettings>>().Value);
                services.AddSingleton<ITokenService, TokenService>();
                services.AddTransient<AuthHeaderHandler>();
                services.AddSingleton<JwtService>();
                services.AddHttpClient<IAuthService, AuthService>().AddHttpMessageHandler<AuthHeaderHandler>();

                services.AddSingleton<INavigationService, NavigationService>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<LoginView>(sp =>
                {
                    var view = new LoginView
                    {
                        DataContext = sp.GetRequiredService<LoginViewModel>()
                    };
                    return view;
                });
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainView>();
                services.AddHttpClient<IAssetTypeService, AssetTypeService>().AddHttpMessageHandler<AuthHeaderHandler>();
                services.AddHttpClient<IStorageLocationService, StorageLocationService>().AddHttpMessageHandler<AuthHeaderHandler>();
                services.AddSingleton<DynamicField>();
                services.AddTransient<AddAssetViewModel>();
                services.AddTransient<AddAssetView>();

                ServiceProvider = services.BuildServiceProvider();

                var loginView = ServiceProvider.GetRequiredService<LoginView>();
                loginView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during application startup: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally { }
        }
    }

}
