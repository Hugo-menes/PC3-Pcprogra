using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PC3_HUGO.Integration.models;

namespace PC3_HUGO.Integration
{
    public class CrearUsuarioApiIntegration
    {
        private readonly ILogger<CrearUsuarioApiIntegration> _logger;
        private const string API_URL="https://reqres.in/api/users";
        private readonly HttpClient httpClient;

        public CrearUsuarioApiIntegration(ILogger<CrearUsuarioApiIntegration> logger){
            _logger = logger;
            httpClient = new HttpClient();
        }

        public async Task<ApiRespuesta> CrearUsuario(string name,string job){
            string requestUrl = $"{API_URL}";

            ApiRespuesta apiRes=null;
            try{
                var userData = new {name,job};
                var jsonUserData=JsonSerializer.Serialize(userData);
                var request= new StringContent(jsonUserData,Encoding.UTF8,"aplication/json");

                HttpResponseMessage response = await httpClient.PostAsync(requestUrl,request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody =  await response.Content.ReadAsStringAsync();
                    apiRes=JsonSerializer.Deserialize<ApiRespuesta>(responseBody);
                }
            }catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return apiRes;
        }
        public class ApiRespuesta
        {
            public string Name { get; set; }
            public string Job { get; set; }	
            public string Id { get; set; }
            public DateTime CreateAt { get; set; }

        }
    }
}