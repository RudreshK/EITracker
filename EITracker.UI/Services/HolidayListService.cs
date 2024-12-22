using EITracker.Models;
using Newtonsoft.Json;
using System.Text;

namespace EITracker.UI.Services
{
    public class HolidayListService
    {
        private readonly HttpClient _httpClient;
        public HolidayListService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<HolidayListModel[]> GetAllHolidaysAsync()
        {
            HolidayListModel[] holidays;
            holidays = await this._httpClient.GetFromJsonAsync<HolidayListModel[]>("api/HolidayLists");
            return holidays;
        }
        public async Task<List<HolidayListModel>> GetAllHolidaysByYearMonthAsync(DateTime dateTime)
        {
            int year = dateTime.Year;
            int month = dateTime.Month;

            string query = $"api/HolidayLists?$filter=year(holidayDate) eq {year} and month(holidayDate) eq {month}";

            List<HolidayListModel> holidays = await this._httpClient.GetFromJsonAsync<List<HolidayListModel>>(query);
            return holidays;
        }

        public async Task<ServiceResponse> DeleteHoliday(Guid holidayId, CancellationToken token)
        {
            string query = $"api/HolidayLists/{holidayId}";
            var res = new ServiceResponse();

            try
            {
                var response = await this._httpClient.DeleteAsync(query);
                // Check the response status
                if (!response.IsSuccessStatusCode)
                {
                    // Deserialize and handle errors
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var exception = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);

                    res.StatusCode = (int)response.StatusCode;
                    res.StatusMessage = exception?.error?.message ?? "An unknown error occurred.";
                }
                else
                {
                    // Operation successful
                    res.StatusCode = (int)response.StatusCode;
                    res.StatusMessage = "Success";
                }
            }
             
            catch (TaskCanceledException)
            {
                res.StatusCode = 408; // Request Timeout
                res.StatusMessage = "Request was canceled or timed out.";
            }
            return res;
        }

        public async Task<ServiceResponse> PostHolidayAsync(HolidayListModel holiday, CancellationToken token)
        {
            var apiUrl = "api/HolidayLists";

            var payloadJson = JsonConvert.SerializeObject(holiday);
            Console.WriteLine($"Payload Sent: {payloadJson}"); // Log payload

            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(payloadJson, Encoding.UTF8, "application/json")
            };

            // Initialize a response object
            var res = new ServiceResponse();

            try
            {
                // Send the request
                var response = await _httpClient.SendAsync(request, token);

                // Check the response status
                if (!response.IsSuccessStatusCode)
                {
                    // Deserialize and handle errors
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var exception = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);

                    res.StatusCode = (int)response.StatusCode;
                    res.StatusMessage = exception?.error?.message ?? "An unknown error occurred.";
                }
                else
                {
                    // Operation successful
                    res.StatusCode = (int)response.StatusCode;
                    res.StatusMessage = "Success";
                }
            }
            catch (TaskCanceledException)
            {
                res.StatusCode = 408; // Request Timeout
                res.StatusMessage = "Request was canceled or timed out.";
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                res.StatusCode = 500; // Internal Server Error
                res.StatusMessage = ex.Message;
            }

            return res;
        }

    }
}
