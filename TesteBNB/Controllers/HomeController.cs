using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace TesteBNB.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
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

        [HttpPost]
        public async Task<IActionResult> Insert(UsuarioModel usuario)
        {
            try
            {
                var jsonUsuario = JsonSerializer.Serialize(usuario);
                var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("UsuarioBNB/Usuario", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UsuarioModel usuario)
        {
            try
            {
                var jsonUsuario = JsonSerializer.Serialize(usuario);
                var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("UsuarioBNB/Usuario", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Index", "Home");
            }
        }



        public async Task<bool> DeleteAPI(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"UsuarioBNB/Usuario/{id}");
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }


        [HttpDelete]
        public ActionResult DeletarUsuario(int id)
        {
            var resultado = DeleteAPI(id);

            return Ok();
        }


    }
}
