using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace VibeTrader.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration["Services:ApiService:Url"] ?? "http://api";
        }

        [HttpGet]
        [Route("{*path}")]
        public async Task<IActionResult> ProxyGet(string path)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/{path}");
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }

        [HttpPost]
        [Route("{*path}")]
        public async Task<IActionResult> ProxyPost(string path)
        {
            var requestContent = await ReadRequestBodyAsync();
            
            var response = await _httpClient.PostAsync(
                $"{_apiBaseUrl}/api/{path}", 
                new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json"));
            
            var content = await response.Content.ReadAsStringAsync();
            
            return Content(content, "application/json");
        }

        [HttpPut]
        [Route("{*path}")]
        public async Task<IActionResult> ProxyPut(string path)
        {
            var requestContent = await ReadRequestBodyAsync();
            
            var response = await _httpClient.PutAsync(
                $"{_apiBaseUrl}/api/{path}", 
                new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json"));
            
            var content = await response.Content.ReadAsStringAsync();
            
            return Content(content, "application/json");
        }

        [HttpDelete]
        [Route("{*path}")]
        public async Task<IActionResult> ProxyDelete(string path)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/{path}");
            var content = await response.Content.ReadAsStringAsync();
            
            return Content(content, "application/json");
        }

        private async Task<string> ReadRequestBodyAsync()
        {
            using var reader = new System.IO.StreamReader(Request.Body);
            return await reader.ReadToEndAsync();
        }
    }
}