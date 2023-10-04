using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using MentorshipCompanionApp.Views.Management;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.ViewModels
{
    public partial class EditUserPageViewModel : BaseViewModel
    {
        private readonly IConnectivity _connectivity;
        private readonly ProfileService _profileService;
        public EditUserPageViewModel(ProfileService profileService, IConnectivity connectivity)
        {
            _profileService = profileService;
            _connectivity = connectivity;

        }
        public ObservableCollection<UserProfile> People { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        int selectedIndex;

        [ObservableProperty]
        string selectedRole;

        [ObservableProperty]
        string searchName;

        [ObservableProperty]
        List<string> roles = new (){ "CDM", "Mentor", "Student" };

        

        [RelayCommand]
        async Task GetPeople(string searchMode = "default") 
        {
            if (IsBusy) { return; }

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue.", $"Check your internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                List<UserProfile> people = new List<UserProfile>();
                if (searchMode == "default")
                {
                    SearchName = string.Empty;
                    people = await _profileService.GetUserProfilesAsync(SelectedRole, SelectedIndex);
                }
                else if (searchMode == "name") 
                {
                    people = await _profileService.GetUserProfilesAsync(SearchName, 4);
                }

                if (People.Count != 0)
                {
                    People.Clear();
                }
                foreach (var person in people)
                {
                    People.Add(person);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get users: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GoToEditDetail(UserProfile user)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            await Shell.Current.GoToAsync($"{nameof(EditUserDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"UserProfile", user}
            });
        }

    }
}
