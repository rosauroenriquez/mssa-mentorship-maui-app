using MentorshipCompanionApp.Models;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Data;
using System.Reflection;

namespace MentorshipCompanionApp.Services
{
    public class ProfileService : BaseService
    {     
        private readonly ActiveUser _activeUser;

        public ProfileService(ActiveUser activeUser)
        {
            _activeUser = activeUser;

            baseAddress = Preferences.Get("BaseAddress", string.Empty) + "api/UserProfile";

        }

        private string baseAddress;

        List<UserProfile> peopleList = new();

        public async Task<UserProfile> GetUserProfileAsync(string userId = null) 
        {
            if (userId == null)
                userId = _activeUser.UserID;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    
                    var url = $"{baseAddress}/{userId}";

                    //TODO: Change how you store and provide Token
                    //var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = this.AuthToken;
                    var response = await httpClient.GetFromJsonAsync<UserProfile>(url);

                    if (response is not null)
                    {
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "Acknowledge");
            }
            finally
            {

            }
            return null;
        }

        public async Task<List<UserProfile>> GetUserProfilesAsync(string role, int index) 
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var baseAddress = Preferences.Get("BaseAddress", string.Empty);
                    var url = $"{baseAddress}api/UserProfile/{role}&{index}";

                    //TODO: Change how you store and provide Token
                    var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retrievedToken);
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        peopleList = await response.Content.ReadFromJsonAsync<List<UserProfile>>();
                    }
                    return peopleList;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "Acknowledge");
            }
            finally
            {

            }
            return null;
        }

        public async Task<bool> UpdateProfileAsync(UserProfile profile) 
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var baseAddress = Preferences.Get("BaseAddress", string.Empty);
                    var url = $"{baseAddress}api/UserProfile";
                    var jsonContent = JsonConvert.SerializeObject(profile);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                    //For HTTPS
                    //var url = "https://localhost:7024/api/UserProfile/";

                    //TODO: Change how you store and provide Token
                    var retrievedToken = Preferences.Get("AuthToken", string.Empty);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retrievedToken);
                    var response = await httpClient.PutAsync(url,content);

                    if (response.IsSuccessStatusCode)
                    {
                        await Shell.Current.DisplayAlert("Success", "User Profile Updated", "OK");
                        return true;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Failed Update", "Update was not successful!", "Acknowledge");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "Acknowledge");
            }
            finally
            {

            }
            return false;
        }
    }
}
