using MentorshipCompanionApp.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Security.Claims;

namespace MentorshipCompanionApp.Services
{
    public class LoginService
    {
        
        ActiveUser _activeUser {  get; set; }
        public LoginService( ActiveUser activeUser)
        {
            _activeUser = activeUser;
        }
        public async Task GetTokenAsync(string email, string password)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                
                    User user = new User(email, password);
                    var jsonContent = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                    var baseAddress = Preferences.Get("BaseAddress", string.Empty);
                    var response = await httpClient.PostAsync($"{baseAddress}api/Token/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        var tokenString = System.Text.Json.JsonSerializer.Deserialize<MSSAToken>(responseContent);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.ReadJwtToken(tokenString.token);

                        Preferences.Set("AuthToken", tokenString.token);
                        
                        IEnumerable<Claim> claims = token.Claims;
                        var userId = claims.First().Value;
                        ActiveToken activeToken = new ActiveToken(token, DateTime.Parse(tokenString.expires));

                        _activeUser.UserID = userId;
                        _activeUser.UserToken = activeToken;
                        return;
                    }
                    else
                    {

                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            await Shell.Current.DisplayAlert("Unauthorized", "Invalid email or password.", "Acknowledge");
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            await Shell.Current.DisplayAlert("Server Error", "An internal server error occurred.", "Acknowledge");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Error", "An error occurred during login.", "Acknowledge");
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                await Shell.Current.DisplayAlert("Network Error", "An error occurred while communicating with the server.", "Acknowledge");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "An unexpected error occurred.", "Acknowledge");
            }
        }
    }
}
