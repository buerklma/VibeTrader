using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VibeTrader.Application.Commands.CreateAlert;
using VibeTrader.Application.Commands.UpdateAlert;
using VibeTrader.Application.DTOs;
using VibeTrader.Client.Services.Interfaces;

namespace VibeTrader.Client.Services
{
    /// <summary>
    /// Service for communicating with the Alerts API
    /// </summary>
    public class AlertApiService : IAlertApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AlertApiService> _logger;
        private readonly string _apiBaseUrl;

        public AlertApiService(HttpClient httpClient, IConfiguration configuration, ILogger<AlertApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiBaseUrl = "api/alerts";
        }

        public async Task<List<AlertDto>> GetAlertsAsync(bool activeOnly = false)
        {
            try
            {
                var queryString = activeOnly ? $"?activeOnly=true" : string.Empty;
                var response = await _httpClient.GetFromJsonAsync<List<AlertDto>>($"{_apiBaseUrl}{queryString}");
                return response ?? new List<AlertDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching alerts");
                throw;
            }
        }

        public async Task<AlertDto> GetAlertByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AlertDto>($"{_apiBaseUrl}/{id}");
                
                if (response == null)
                    throw new Exception($"Alert with ID {id} not found");
                    
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching alert with ID {AlertId}", id);
                throw;
            }
        }

        public async Task<AlertDto> CreateAlertAsync(CreateAlertCommand command)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, command);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AlertDto>() ?? 
                       throw new Exception("Failed to deserialize created alert");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating alert for symbol {Symbol}", command.Symbol);
                throw;
            }
        }

        public async Task<AlertDto> UpdateAlertAsync(UpdateAlertCommand command)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{command.Id}", command);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AlertDto>() ?? 
                       throw new Exception("Failed to deserialize updated alert");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating alert with ID {AlertId}", command.Id);
                throw;
            }
        }

        public async Task DeleteAlertAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting alert with ID {AlertId}", id);
                throw;
            }
        }
    }
}