using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CropDealBackend.Controllers
{
    [Route("api/gateway/auth")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(HttpClient httpClient, IConfiguration configuration, ILogger<GatewayController> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        // 🔹 Forward Login Request to JWT Microservice
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] object loginRequest)
        {
            try
            {
                string authServiceBaseUrl = _configuration["Microservices:JWTMicroservice"];
                if (string.IsNullOrEmpty(authServiceBaseUrl))
                {
                    return StatusCode(500, new { message = "JWT Microservice URL is not configured" });
                }

                string authServiceUrl = $"{authServiceBaseUrl.TrimEnd('/')}/api/auth/login";
                _logger.LogInformation($"Forwarding register request to: {authServiceUrl}");

                var response = await _httpClient.PostAsync(authServiceUrl,
                    new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, responseContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling Login API: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // 🔹 Forward Register Request to JWT Microservice
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] object registerRequest)
        {
            try
            {
                string authServiceBaseUrl = _configuration["Microservices:JWTMicroservice"];
                if (string.IsNullOrEmpty(authServiceBaseUrl))
                {
                    return StatusCode(500, new { message = "JWT Microservice URL is not configured" });
                }

                string authServiceUrl = $"{authServiceBaseUrl.TrimEnd('/')}/api/auth/register";
                _logger.LogInformation($"Forwarding register request to: {authServiceUrl}");


                var response = await _httpClient.PostAsync(authServiceUrl,
                    new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json"));

                string responseContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, responseContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling Register API: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
