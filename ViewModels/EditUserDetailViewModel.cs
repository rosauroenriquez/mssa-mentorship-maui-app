using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.ViewModels
{
    [QueryProperty("UserProfile", "UserProfile")]
    public partial class EditUserDetailViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;

        private readonly UserManagementService _userManagementService;
        public EditUserDetailViewModel(ProfileService profileService, UserManagementService userManagementService)
        {
            _profileService = profileService;
            _userManagementService = userManagementService;

        }

        [ObservableProperty]
        UserProfile userProfile;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        bool isEditEnabled = false;

        [ObservableProperty]
        bool isChecked = false;
        
        bool canEdit() 
        {
            return !IsEditEnabled;
        }

        [RelayCommand(CanExecute =(nameof(canEdit)))]
        void Edit() 
        {
            IsEditEnabled = true;
        }

        [RelayCommand(CanExecute =(nameof(IsEditEnabled)))]
        async Task Save()
        {
            IsBusy = true;
            await _profileService.UpdateProfileAsync(UserProfile);
            
            if (IsChecked) 
            {
                //Call UserCredential Service and flagged NeedsPasswordReset
                await _userManagementService.ResetPasswordAsync(UserProfile.UserID);
            }
            
            IsEditEnabled = false;
            IsBusy = false;
        }
    }
}
