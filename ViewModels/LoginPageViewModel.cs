using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using MentorshipCompanionApp.Views.Dashboard;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.ViewModels
{
    
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IConnectivity _connectivity;
        private readonly LoginService _loginService;

        ActiveUser _activeUser;

        
        public LoginPageViewModel(LoginService loginService, ActiveUser activeUser, IConnectivity connectivity)
        {
            Title = "Login";
            _loginService = loginService;
            activeUser.UserToken = null;
            _activeUser = activeUser;
            _connectivity = connectivity;
        }

        public SecurityToken token {  get; }

        [ObservableProperty]
        string emailAddress;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task GetTokenAsync() 
        {
            IsBusy = true;

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Connection Required", $"Check your internet and try again.", "OK");
                    return;
                }
                await _loginService.GetTokenAsync(EmailAddress, Password);
            }
            catch (OperationCanceledException opCanEx)
            {
                await Shell.Current.DisplayAlert("Some Exception", opCanEx.Message, "OK");
            }
            IsBusy = false;

            if (_activeUser.UserToken != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}",true);
            }
            else
                await Shell.Current.DisplayAlert("Login Status", "FAILED", "OK");

            return;
        }

    }
}
