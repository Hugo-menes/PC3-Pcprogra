using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PC3_HUGO.Integration.models;

namespace PC3_HUGO.Integration
{
    public class UsuariosApiIntegration
    {
        private readonly ILogger<UsuariosApiIntegration> _logger;
        private const string API_URL="https://reqres.in/api/users";
        private readonly HttpClient httpClient;

        public UsuariosApiIntegration(ILogger<UsuariosApiIntegration> logger){
            _logger = logger;
            httpClient = new HttpClient();
        }

        public async Task<List<Usuario>> GetAllUsuarios(){
            string requestUrl = $"{API_URL}";
            List<Usuario> listado = new List<Usuario>();
            try{
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiRespuesta =  await response.Content.ReadFromJsonAsync<Api>();
                    if(apiRespuesta != null){
                        listado=apiRespuesta.Data?? new List<Usuario>();
                    }
                }
            }catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return listado;
        }
        class Api
        {
            public int Page { get; set; }	
            public int PerPage { get; set; }
            public int Total { get; set; }
            public int TotalPages { get; set; }
            public List<Usuario> Data { get; set; }
            public Support support { get; set; }

        }
    }
}