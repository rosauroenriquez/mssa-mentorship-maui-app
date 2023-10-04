using MentorshipCompanionApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Services
{
    public class MentorshipService : BaseService
    {
        private readonly ActiveUser _activeUser;
        public MentorshipService(ActiveUser activeUser)
        {
            _activeUser = activeUser;
            baseAddress = Preferences.Get("BaseAddress", string.Empty) + "api/Mentorship";
        }

        private string baseAddress;

        public async Task<bool> AddMentorAsync(string studentId, string mentorId) 
        {
            Mentorship mentorship = new Mentorship 
            {
                StudentID = studentId,
                MentorID = mentorId
            };
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var url = baseAddress;
                    var jsonContent = JsonConvert.SerializeObject(mentorship);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    //For HTTPS
                    //var url = "https://localhost:7024/api/UserProfile/" + _activeUser.UserID;

                    //TODO: Change how you store and provide Token
                    //var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = this.AuthToken;
                    var response = await httpClient.PostAsync(url,content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Unexpected Error!", ex.Message, "Acknowledge");
            }
            finally
            {

            }
            return false;
        }

        public async Task<IEnumerable<Mentorship>> GetMentorsAsync(string studentId)
        {
            //Just needs student id

            try 
            { 
                using ( var httpClient = new HttpClient()) 
                {
                    var url = baseAddress + $"?userId={studentId}";
                    httpClient.DefaultRequestHeaders.Authorization = this.AuthToken;
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<Mentorship>>(url);

                    if (response == null) 
                    {
                        await Shell.Current.DisplayAlert("Mentors", "No Mentors found on record", "OK");
                        
                    }

                    return response;
                }
            } 
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Unexpected Error!", ex.Message, "Acknowledge");
            } 
            finally 
            { 
            
            }
            return null;
        }

        //public async Task<IEnumerable<UserProfile>> GetMentorsAsync()
        //{
        //    //Just needs student id
        //}
    }
}
