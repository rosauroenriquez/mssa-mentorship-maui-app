using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using MentorshipCompanionApp.Views.Interaction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.ViewModels
{
    
    public partial class HomePageViewModel : BaseViewModel
    {
        
        ProfileService _profileService;
        
        MentorshipService _mentorshipService;

        GoalService _goalService;

        public HomePageViewModel(ProfileService profileService, MentorshipService mentorshipService, GoalService goalService)
        {

            _profileService = profileService;

            _mentorshipService = mentorshipService;

            _goalService = goalService;

            LoadProfileCommand.Execute(this);

        }        

        [ObservableProperty]
        UserProfile activeProfile;

        [ObservableProperty]
        public ObservableCollection<UserProfile> mentors = new ObservableCollection<UserProfile>();

        [ObservableProperty]
        public ObservableCollection<Goal> goals = new ObservableCollection<Goal>();

        [RelayCommand]
        async Task LoadProfileAsync()
        {
            ActiveProfile = await _profileService.GetUserProfileAsync();

            if (ActiveProfile is not null)
            {
                Title = $"Welcome {ActiveProfile.FirstName}";
                await GetMentorsAsync();
                await GetGoalsAsync();
            }
            else
                await Shell.Current.DisplayAlert("Profile", "Profile Not Found", "OK");
            return;
        }

        async Task GetMentorsAsync()
        {
            Mentors.Clear();
            var mentors = await _mentorshipService.GetMentorsAsync(ActiveProfile.UserID);
            foreach (var mentor in mentors)
            {
                var mentorProfile = await _profileService.GetUserProfileAsync(mentor.MentorID);
                if (mentorProfile != null)
                {
                    Mentors.Add(mentorProfile);
                }
            }
        }

        async Task GetGoalsAsync()
        {
            Goals.Clear();
            var goals = await _goalService.GetGoalsAsync(ActiveProfile.UserID,"studentId");
            foreach (var goal in goals)
            {
                Goals.Add(goal);
            }
        }

        [RelayCommand]
        async Task GoToGoalDetail(Goal goal)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            await Shell.Current.GoToAsync($"{nameof(GoalDetailsPage)}", true,
            new Dictionary<string, object>
            {
                {"Goal", goal}
            });
        }

    }
}
