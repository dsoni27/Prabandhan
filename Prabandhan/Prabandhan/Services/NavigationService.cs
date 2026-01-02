using Microsoft.Extensions.DependencyInjection;
using Parbandhan.Interfaces;
using Parbandhan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parbandhan.Services
{
    public class NavigationService : INavigationService
    {
        public void ShowMainView()
        {
            var mainView = App.ServiceProvider.GetRequiredService<MainView>();
            mainView.Show();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = mainView;
        }
    }
}
