using Parbandhan.Interfaces;
using Parbandhan.Models;
using Parbandhan.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parbandhan.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Private Fields
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly INavigationService _navigationService;
        private string _email;
        private string _password;
        private string _secureText;

        //Public Properties
        //UI-Bound Properties, Commands and Methods to be implemented  
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ValidateLogin(nameof(Email), value);
            }
        }
        [Required(ErrorMessage = "Password is required!")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ValidateLogin(nameof(Password), value);
            }
        }

        public string ModuleName { get; set; } = "Parbandhan";

        public string ErrorMessage { get; set; }
        public bool IsBusy { get; set; }

        public ViewModelCommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService, ITokenService tokenService, INavigationService navigationService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _navigationService = navigationService;

            LoginCommand = new ViewModelCommand(async _ => await LoginAsync(), CanLogin);

        }

        private void ValidateLogin(string propertyName, object value)
        {
            Validate(propertyName, value);
            LoginCommand.RaiseCanExecuteChanged();
        }

        private bool CanLogin(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }

        private async Task LoginAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var request = new LoginRequestModel
                {
                    Email = Email,
                    Password = Password,
                    ModuleName = ModuleName
                };

                var response = await _authService.LoginAsync(request);

                if (response.Result == null)
                {
                    ErrorMessage = "Invalid email or password!";
                    IsBusy = false;
                    MessageBox.Show(ErrorMessage, "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Navigate
                _navigationService.ShowMainView();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred during login: {ex.Message}";
                MessageBox.Show(ErrorMessage, "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}