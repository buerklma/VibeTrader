using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VibeTrader.Application.DTOs;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Client.Services
{
    /// <summary>
    /// Implementation of the IAlertService that communicates with the API
    /// </summary>
    public class AlertService : IAlertService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/alerts";

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public AlertService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <inheritdoc />
        public async Task<List<AlertDto>> GetAlertsAsync(bool activeOnly = false)
        {
            try
            {
                var url = activeOnly ? $"{BaseUrl}?activeOnly=true" : BaseUrl;
                var response = await _httpClient.GetFromJsonAsync<List<AlertDto>>(url);
                return response ?? new List<AlertDto>();
            }
            catch (Exception)
            {
                // In a real application, we would log this error and provide better error handling
                return new List<AlertDto>();
            }
        }

        /// <inheritdoc />
        public async Task<AlertDto> GetAlertByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<AlertDto>($"{BaseUrl}/{id}") 
                ?? throw new Exception($"Failed to retrieve alert with ID {id}");
        }

        /// <inheritdoc />
        public async Task<AlertDto> CreateAlertAsync(string symbol, decimal targetPrice, AlertType type)
        {
            var request = new
            {
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type
            };

            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AlertDto>()
                ?? throw new Exception("Failed to deserialize created alert");
        }

        /// <inheritdoc />
        public async Task<AlertDto> UpdateAlertAsync(Guid id, string symbol, decimal targetPrice, AlertType type)
        {
            var request = new
            {
                Id = id,
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type
            };

            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AlertDto>()
                ?? throw new Exception("Failed to deserialize updated alert");
        }

        /// <inheritdoc />
        public async Task DeleteAlertAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}