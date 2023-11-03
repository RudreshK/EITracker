using EITracker.Models;
using Microsoft.AspNetCore.OData.Deltas;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Text;

namespace EITracker.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            List<UserModel> users = new List<UserModel>();
            var apiusers = await this._httpClient.GetFromJsonAsync<List<UserModel>>("api/Users");

            return apiusers;
        }
        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            return await this._httpClient.GetFromJsonAsync<UserModel>("api/Users(" + userId + ")"); 
        }
        public async Task PatchUserAsync(UserModel user, CancellationToken token) 
        {              
                // Define the request URL with the appropriate parameters
                var apiUrl = "api/Users(" + user.Id + ")";

                // Create the request message
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), apiUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                // Send the request and await the response
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    GetAllUsersAsync();
                }
                else
                {
                    // Handle error here
                }
           
        }
    }
}
