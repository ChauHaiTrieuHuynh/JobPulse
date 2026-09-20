using JobPulse.Service.Services;
using JobPulse.Service.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public class LinkedInJobSource : IJobSource
    {
        private readonly HttpClient _httpClient;
        private readonly LinkedInSettings _settings;
        private readonly ILogger _logger;


        public LinkedInJobSource(HttpClient httpClient, IOptions<LinkedInSettings> options, ILogger<LinkedInJobSource> logger)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _logger = logger;
        }

        public async Task<string> FetchJobAsync()
        {
            try
            {
                string keyword = _settings.Keywords.First();

                string url = BuildUrl(keyword);

                HttpResponseMessage response =
                    await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        private string BuildUrl(string keyword)
        {
            string result = $"{_settings.BaseUrl}" +
           $"keywords={Uri.EscapeDataString(keyword)}" +
           $"&location={Uri.EscapeDataString(_settings.Location)}" +
           $"&geoId={_settings.GeoId}" +
           $"&f_TPR={_settings.TimeRange}" +
           $"&position=1" +
           $"&pageNum=0";

            _logger.LogInformation("Url {result} ", result);

            return result;
        }
    }
}
