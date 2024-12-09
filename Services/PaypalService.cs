using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GroceryManagement1.Services
{
    public class PaypalService
    {
        private static readonly string clientId = "AS9-_3i1eBN_I0FJNCwX3OK-JrypOlCn3Z8lANul6F4BX3Jygm_4Xgzcdb9ELSUXapRR_UkZ43QTUdSt";// Replace with your PayPal Client ID
        private static readonly string clientSecret = "EL8Q3oTx1Dz5o-LfNZeFuNmRBtL42M9KmyFMo7N0dBWSJbD7qrQ65thjsYKxxh7o09yr8EehJLwOfRGA";// Replace with your PayPal Secret
        private static readonly string url = "https://api-m.sandbox.paypal.com/v1/oauth2/token"; // Use 'api-m.paypal.com' for live

        public async Task<string> GetAccessTokenAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // Set a timeout for the HTTP request
                httpClient.Timeout = TimeSpan.FromSeconds(30);  // Set timeout to 30 seconds (adjust as needed)

                // Basic authentication for client ID and secret
                var byteArray = Encoding.ASCII.GetBytes($"AS9-_3i1eBN_I0FJNCwX3OK-JrypOlCn3Z8lANul6F4BX3Jygm_4Xgzcdb9ELSUXapRR_UkZ43QTUdSt:EL8Q3oTx1Dz5o-LfNZeFuNmRBtL42M9KmyFMo7N0dBWSJbD7qrQ65thjsYKxxh7o09yr8EehJLwOfRGA");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Prepare the request payload
                var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                try
                {
                    // Send the request and await the result
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    // Ensure success status code
                    response.EnsureSuccessStatusCode();

                    // Parse the response
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenObj = JObject.Parse(jsonResponse);
                    return tokenObj["access_token"].ToString();
                }
                catch (Exception ex)
                {
                    // Handle exception (network issues, timeout, etc.)
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}