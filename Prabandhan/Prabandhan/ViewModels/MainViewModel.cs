using FontAwesome.Sharp;
using Microsoft.Extensions.DependencyInjection;
using Parbandhan.Interfaces;
using Parbandhan.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parbandhan.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;
        private string? _caption;
        private IconChar _icon;
        private string _loggedInUserName;
        private string _email;
        private readonly ITokenService _tokenService;
        private readonly IServiceProvider _serviceProvider;

        //Properties
        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string? Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public string LoggedInUserName => _tokenService.UserClaims?.Name;

        public string Email => _tokenService.UserClaims?.Email;

        //Commands
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowAddAssetCommand { get; }
        public ICommand ShowManageAssetsCommand { get; }
        public ICommand ShowManageBucketCommand { get; }

        public MainViewModel(ITokenService tokenService, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _tokenService.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_tokenService.UserClaims))
                {
                    OnPropertyChanged(nameof(LoggedInUserName));
                    OnPropertyChanged(nameof(Email));
                }
            };

            //Initialize Commands
            ShowDashboardCommand = new ViewModelCommand(ExecuteShowDashboardCommand);
            ShowAddAssetCommand = new ViewModelCommand(ExecuteShowAddAssetCommand);
            ShowManageAssetsCommand = new ViewModelCommand(ExecuteShowManageAssetsCommand);
            ShowManageBucketCommand = new ViewModelCommand(ExecuteShowManageBucketCommand);

            //Default View
            ExecuteShowDashboardCommand(null);
            _serviceProvider = serviceProvider;
        }

        private void ExecuteShowDashboardCommand(object? obj)
        {
            CurrentChildView = new DashboardViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowAddAssetCommand(object? obj)
        {
            CurrentChildView = _serviceProvider.GetRequiredService<AddAssetViewModel>();
            Caption = "Add Asset";
            Icon = IconChar.Video;
        }

        private void ExecuteShowManageAssetsCommand(object? obj)
        {
            CurrentChildView = new ManageAssetsViewModel();
            Caption = "Manage Assets";
            Icon = IconChar.BoxesStacked;
        }

        private void ExecuteShowManageBucketCommand(object? obj)
        {
            CurrentChildView = new ManageBucketViewModel();
            Caption = "Manage Bucket";
            Icon = IconChar.Bucket;
        }
    }
}
