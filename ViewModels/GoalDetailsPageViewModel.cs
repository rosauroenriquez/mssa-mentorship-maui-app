using CommunityToolkit.Mvvm.ComponentModel;
using MentorshipCompanionApp.Models;
using MentorshipCompanionApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.ViewModels
{
    [QueryProperty("Goal","Goal")]
    public partial class GoalDetailsPageViewModel : BaseViewModel
    {
        private readonly GoalService _goalService;
        public GoalDetailsPageViewModel(GoalService goalService) 
        {
            _goalService = goalService;
        }

        [ObservableProperty]
        Goal goal;

        [ObservableProperty]
        ObservableCollection<ChatMessage> mentorNotes;

        public async Task GetMentorNotes() 
        {
            MentorNotes = await _goalService.GetMentorNotesAsync(Goal.GoalThread.ThreadId);
        }
    }
}
