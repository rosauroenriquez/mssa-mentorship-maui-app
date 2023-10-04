using MentorshipCompanionApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Services
{
    public class GoalService : BaseService
    {
        public GoalService()
        {
            baseAddress = Preferences.Get("BaseAddress", string.Empty) + "api/Goal";
        }

        private string baseAddress;

        public async Task<IEnumerable<Goal>> GetGoalsAsync(string searchInput, string searchType)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{baseAddress}/{searchInput}&{searchType}";
                    httpClient.DefaultRequestHeaders.Authorization = this.AuthToken;
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var goalsJson = await response.Content.ReadAsStringAsync();
                        var goals = JsonConvert.DeserializeObject<List<Goal>>(goalsJson);
                        return goals;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("No Goals Found", "Set some goals and go for it!", "Let's Go!");
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Goals Error", "Something went wrong while loading goals.", "Acknowledge");
            }
            return null;
        }

        public async Task<ObservableCollection<ChatMessage>> GetMentorNotesAsync(int threadId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"http://localhost:5013/api/ChatMessage?threadId={threadId}";
                    httpClient.DefaultRequestHeaders.Authorization = this.AuthToken;
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var goalsJson = await response.Content.ReadAsStringAsync();
                        var goals = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(goalsJson);
                        return goals;
                    }
                    else
                    {
                        //await Shell.Current.DisplayAlert("No Goals Found", "Set some goals and go for it!", "Let's Go!");
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Goals Error", "Something went wrong while loading goals.", "Acknowledge");
            }
            return null;
        }
    }
}
