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
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("UsuarioBNB");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                List<UsuarioModel>? usuarios = JsonSerializer.Deserialize<List<UsuarioModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(usuarios);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }


        public async Task<IActionResult> Cadastrar()
        {

            HttpResponseMessage response = await _httpClient.GetAsync("UsuarioBNB/EnderecosComerciais");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            List<EnderecoModel>? endereco = JsonSerializer.Deserialize<List<EnderecoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(endereco);
        }


    }
}