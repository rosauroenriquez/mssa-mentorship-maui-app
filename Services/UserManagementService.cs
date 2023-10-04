using MentorshipCompanionApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Services
{
    public class UserManagementService
    {
        private readonly ActiveUser _activeUser;

        public UserManagementService(ActiveUser activeUser)
        {
            _activeUser = activeUser;
        }

        public async Task<bool> CreateUserAsync(NewUser newUser) 
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = "http://localhost:5013/api/UserProfile";
                    var jsonContent = JsonConvert.SerializeObject(newUser);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                    var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retrievedToken);
                    var response = await httpClient.PostAsync(url, content);

                    
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    //TODO: Future Improvement: Notify what exactly went wrong here
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Unexpected Error", $"{ex.Message}", "OK");
            }
            return false;
        }

        public async Task<bool> ResetPasswordAsync(string userId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"http://localhost:5013/api/UserCredentials/{userId}&true";
                    var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retrievedToken);
                    var response = await httpClient.PutAsync(url,null);


                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    //TODO: Future Improvement: Notify what exactly went wrong here
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Unexpected Error", $"{ex.Message}", "OK");
            }
            return false;
        }
    }
}
