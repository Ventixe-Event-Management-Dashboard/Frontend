using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Authentication.Services
{
    public class SignUpService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _sendUrl;
        private readonly string? _verifyUrl;

        public SignUpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _sendUrl = config["EmailVerification:SendUrl"];
            _verifyUrl = config["EmailVerification:VerifyUrl"];
        }

        public async Task<bool> SendVerificationCodeAsync(string email)
        {
            //var payload = new { Email = email };
            //var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync(_sendUrl, content);

            //return response.IsSuccessStatusCode;

            return true;
        }

        public async Task<bool> VerifyCodeAsync(string email, string code)
        {
            //var payload = new { Email = email, Code = code };
            //var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync(_verifyUrl, content);

            //return response.IsSuccessStatusCode;

            return true;
        }
    }
}
