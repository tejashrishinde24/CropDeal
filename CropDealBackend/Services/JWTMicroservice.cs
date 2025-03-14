namespace CropDealBackend.Services
{
    public class JWTMicroservice
    {
        private readonly HttpClient _httpClient;

        public JWTMicroservice(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("JWTMicroservice");
        }

        public async Task<string> GetCropsAsync()
        {
            var response = await _httpClient.GetAsync("/api/auth");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
