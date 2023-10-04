using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MentorshipCompanionApp.ViewModels
{
      

    public partial class SearchPageViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;

        private readonly IConnectivity _connectivity;

        private readonly MentorshipService _mentorshipService;

        public SearchPageViewModel(ProfileService profileService, IConnectivity connectivity, MentorshipService mentorshipService)
        {
            _profileService = profileService;
            _connectivity = connectivity;
            _mentorshipService = mentorshipService;
        }

        public ObservableCollection<UserProfile> People { get; set; } = new();

        public ObservableCollection<UserProfile> PeopleByRole { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string searchName;

        [ObservableProperty]
        string searchRole;

        [ObservableProperty]
        UserProfile selectedStudent;

        [ObservableProperty]
        UserProfile selectedMentor;

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
                    //SearchName = string.Empty;
                    people = await _profileService.GetUserProfilesAsync(SearchRole , 0);
                }

                if (PeopleByRole.Count != 0)
                {
                    PeopleByRole.Clear();
                    People.Clear();
                }
                foreach (var person in people)
                {
                    PeopleByRole.Add(person);
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
        void Search()
        {
            People.Clear();
            if (!string.IsNullOrEmpty(SearchName))
            {                
                foreach (var person in PeopleByRole)
                {
                    if (person.FirstName.Contains(SearchName, StringComparison.OrdinalIgnoreCase) || 
                        person.LastName.Contains(SearchName, StringComparison.OrdinalIgnoreCase))
                        People.Add(person);
                }
            }
            else
            {
                IsRefreshing = true;
            }
        }

        [RelayCommand]
        async Task Cancel() 
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task Select()
        {
            
            if (SearchRole == "Mentor") 
            {
                if (SelectedMentor is null || SelectedStudent is null)
                {
                    return;
                }
                if (await _mentorshipService.AddMentorAsync(SelectedStudent.UserID, SelectedMentor.UserID))
                {
                    await Shell.Current.DisplayAlert("Success!", $"{SelectedMentor.FirstName} {SelectedMentor.LastName} " +
                        $"was added as mentor for {SelectedStudent.FirstName} {SelectedStudent.LastName}", "Acknowledge");
                    await Shell.Current.GoToAsync("..", true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Failed", "Mentor was not added", "OK");
                }
            }
            else
                await Shell.Current.GoToAsync("..", true);

        }

        [RelayCommand]
        void Selected(UserProfile selectedUser)
        {
            if (SearchRole == "Student")
                SelectedStudent = selectedUser;
            else if (SearchRole == "Mentor")
                SelectedMentor = selectedUser;
        }
    }

    
}
