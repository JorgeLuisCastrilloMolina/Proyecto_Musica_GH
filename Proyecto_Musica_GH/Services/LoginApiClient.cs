using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Proyecto_Musica_GH.Models;

namespace Proyecto_Musica_GH.Services
{
    public class LoginApiClient
    {
        private readonly HttpClient _httpClient;

        public LoginApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/login", request);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UsuarioResponse>();
        }

        public async Task<UsuarioResponse?> RegisterAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("/register", usuario);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UsuarioResponse>();
        }
    }
}