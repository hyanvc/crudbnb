using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using TesteBNB.Models;

namespace TesteBNB.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            // Configurar o HttpClient para chamar a API
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7100/");
        }

        public async Task<IActionResult> Index()
        {
            string parametro = null;
            var result = await ResultAPI(parametro);
            return View(result);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var parametro = "EnderecosComerciais";
            var result = await ResultAPI(parametro);
            return View(result);
        }

        public async Task<List<UsuarioModel>?> ResultAPI(string parametro)
        {
            try
            {
                HttpResponseMessage response = null;
                if (parametro == null)
                {
                    response = await _httpClient.GetAsync("UsuarioBNB");
                    response.EnsureSuccessStatusCode();
                }
                else
                {
                    response = await _httpClient.GetAsync("UsuarioBNB/" + parametro);
                    response.EnsureSuccessStatusCode();
                }

                string content = await response.Content.ReadAsStringAsync();
                List<UsuarioModel>? result = JsonSerializer.Deserialize<List<UsuarioModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (HttpRequestException)
            {
                return new List<UsuarioModel>();
            }
        }
    }
}
