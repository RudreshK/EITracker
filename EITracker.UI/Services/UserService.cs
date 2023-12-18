using EITracker.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
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

        public async Task<List<string>> GetAllRolesAsync()
        {
            List<string> roles = new List<string>();
             roles =  await this._httpClient.GetFromJsonAsync<List<string>>($"api/Users({ Guid.Empty})/roles");
            return roles;
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
                var response = await _httpClient.SendAsync(request,token);

        }
        public async Task<ServiceResponse> PostUserAsync(UserModel user, CancellationToken token)
        {

            var apiUrl = "api/Users";

            // Create the request message
            var request = new HttpRequestMessage(new HttpMethod("POST"), apiUrl);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request,token);
            ServiceResponse res = new ServiceResponse();
            if (!response.IsSuccessStatusCode)
            {
                res.StatusCode=(int) response.StatusCode;
                ErrorResponse exception = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                res.StatusMessage = exception.error.message;
            }
            return res;      
        }
    }
}
