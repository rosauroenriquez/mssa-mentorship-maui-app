using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using MentorshipCompanionApp.Views.Search;
using System.Collections.ObjectModel;

namespace MentorshipCompanionApp.ViewModels
{
    [QueryProperty("UserProfile", "MentorToBeAdded")]
    public partial class MentorshipPageViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;

        private readonly IConnectivity _connectivity;

        private readonly SearchPageViewModel _searchPageViewModel;

        private readonly MentorshipService _mentorshipService;


        //Inject Search Page
        public MentorshipPageViewModel(ProfileService profileService, IConnectivity connectivity,
            SearchPageViewModel searchPageViewModel, MentorshipService mentorshipService)
        {
            _profileService = profileService;
            _connectivity = connectivity;
            _searchPageViewModel = searchPageViewModel;
            _mentorshipService = mentorshipService;
        }
        public ObservableCollection<UserProfile> Mentors { get; set; } = new();

        [ObservableProperty]
        UserProfile selectedStudent;

        

        //TODO: Call Search Page and Return the User
        //Bind it on the Student Card View
        [RelayCommand]
        async Task Search() 
        {
            _searchPageViewModel.SearchRole = "Student";
            await Shell.Current.GoToAsync($"{nameof(SearchPage)}", true);            
        }

        [RelayCommand]
        async Task GetStudentFromSearch() 
        {
            if (_searchPageViewModel.SelectedStudent != null) 
            {
                SelectedStudent = _searchPageViewModel.SelectedStudent;
                await GetMentorsAsync();
            }
            
        }

        async Task GetMentorsAsync() 
        {
            Mentors.Clear();
            var mentors = await _mentorshipService.GetMentorsAsync(SelectedStudent.UserID);
            foreach (var mentor in mentors) 
            {
                var mentorProfile = await _profileService.GetUserProfileAsync(mentor.MentorID);
                if (mentorProfile != null) 
                {
                    Mentors.Add(mentorProfile);
                }
            }
        }

        //TODO: Add Mentor Command
        //Call Search Page, Return Mentor List
        [RelayCommand]
        async Task AddMentor() 
        {
            _searchPageViewModel.SearchRole = "Mentor";
            await Shell.Current.GoToAsync($"{nameof(SearchPage)}", true);
        }


    }
}
