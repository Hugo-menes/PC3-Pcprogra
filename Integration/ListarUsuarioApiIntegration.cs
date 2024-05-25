using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PC3_HUGO.Integration.models;

namespace PC3_HUGO.Integration
{
    public class ListarUsuarioApiIntegration
    {
        private readonly ILogger<ListarUsuarioApiIntegration> _logger;
        private const string API_URL="https://reqres.in/api/users/";
        private readonly HttpClient httpClient;

        public ListarUsuarioApiIntegration(ILogger<ListarUsuarioApiIntegration> logger){
            _logger = logger;
            httpClient = new HttpClient();
        }

        public async Task<Usuario> GetUsuario(int Id){
            string requestUrl = $"{API_URL}{Id}";
            Usuario usuario = new Usuario();
            try{
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiRespuesta =  await response.Content.ReadFromJsonAsync<ApiUsuario>();
                    if(apiRespuesta != null){
                        usuario=apiRespuesta.Data?? new Usuario();
                    }
                }
            }catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return usuario;
        }
        class ApiUsuario
        {
            public Usuario Data { get; set; }
            public Support Support { get; set; }

        }
    }
}