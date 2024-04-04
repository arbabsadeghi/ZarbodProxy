using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace ZarbodProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : Controller
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        public AssetController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this._configuration = configuration;
        }

        [HttpGet("types")]
        public async Task<IActionResult> types()
        {
            string apiUrl = _configuration.GetValue<string>("url") + "types";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                // Forward the successful response back to the client
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            else
            {
                // Handle the error response here
                return StatusCode((int)response.StatusCode, "Error calling the external API");
            }
        }


        [HttpPut("inquiryRequest")]
        public async Task<IActionResult> inquiryRequest([FromBody] InquiryRequestDto data)
        {
            string apiUrl = _configuration.GetValue<string>("url") + "inquiryRequest";

            // Serialize the request data to JSON
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Send a PUT request to the external API
            HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var apiResponse = await response.Content.ReadAsStringAsync();

                // Forward the successful response back to the client
                return Ok(apiResponse);
            }
            else
            {
                // Forward the error response back to the client
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

        [HttpPost("/api/Asset/inquiry")]
        public async Task<IActionResult> inquiry([FromBody] InquiryDto data)
        {
            string apiUrl = _configuration.GetValue<string>("url") + "inquiry";

            // Serialize the request data to JSON
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Send a POST request to the external API
            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var apiResponse = await response.Content.ReadAsStringAsync();

                // Forward the successful response back to the client
                return Ok(apiResponse);
            }
            else
            {
                // Forward the error response back to the client
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }
}
