using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MentorshipCompanionApp.Services;

namespace MentorshipCompanionApp.ViewModels
{
    public partial class AddUserPageViewModel : BaseViewModel
    {
        private readonly ActiveUser _activeUser;

        private readonly UserManagementService _userManagementService;
        public AddUserPageViewModel(ActiveUser activeUser, UserManagementService userManagementService)
        {
            Title = "Add User";
            Role = new List<string> { "MSSA Admin", "CDM", "Student", "Mentor" };
            _activeUser = activeUser;
            _userManagementService = userManagementService;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string userID;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string emailAddress;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string firstName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string lastName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string linkedInURL;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string selectedRole;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        string jobTitle;

        [ObservableProperty]
        List<string> role;

        [RelayCommand(CanExecute = (nameof(CanExecute)))]
        async Task AddUserAsync() 
        {

            UserCredentials userCredentials = new UserCredentials()
            {
                userID = UserID,
                emailAddress = EmailAddress,
                hashedPassword = $"default.{SelectedRole}",
                role = SelectedRole,
                accountStatus = "Active",
                needPasswordReset = true                
            };

            NewUser newUser = new NewUser()
            {
                userID = UserID,
                firstName = FirstName, 
                lastName = LastName,
                linkedInURL = LinkedInURL,
                jobTitle = JobTitle,
                userCredentials = userCredentials
            };

            
            try 
            {
                var creationStatus = await _userManagementService.CreateUserAsync(newUser);
                //using (var httpClient = new HttpClient())
                //{
                //    var url = "http://localhost:5013/api/UserProfile";
                //    var jsonContent = JsonConvert.SerializeObject(newUser);
                //    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _activeUser.UserToken.Token.UnsafeToString());
                //    var response = await httpClient.PostAsync(url, content);

                //    if (response.IsSuccessStatusCode) 
                //    {
                //        await Shell.Current.DisplayAlert("User Created", $"{FirstName} {LastName} account created.", "OK");
                //        Clear();
                //    }
                //    else
                //        await Shell.Current.DisplayAlert("User Creation Failed", $"{response.Content}", "OK");
                //}

                if (creationStatus) 
                {
                    await Shell.Current.DisplayAlert("User Created", $"{FirstName} {LastName} account created.", "OK");
                    Clear();
                }
                else
                    await Shell.Current.DisplayAlert("User Creation Failed", $"Something went wrong", "OK");
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("Unexpected Error", $"{ex.Message}", "OK");
            }
            return;
        }

        [RelayCommand]
        void Clear()
        {
            UserID = null;
            EmailAddress = null;
            FirstName = null; 
            LastName = null;
            LinkedInURL = null;
            JobTitle = null;
            SelectedRole = null;
            return;
        }
        
        bool CanExecute() 
        {
            if (string.IsNullOrEmpty(UserID) ||
                string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(EmailAddress) ||
                string.IsNullOrEmpty(SelectedRole) ||
                string.IsNullOrEmpty(LinkedInURL))
                return false;
            return true;
        }
    }
}
