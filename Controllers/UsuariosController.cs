using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PC3_HUGO.Integration;
using PC3_HUGO.Integration.models;

namespace PC3_HUGO.Controllers
{

    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ListarUsuariosApiIntegration _apiListarUsers;

        public UsuariosController(ILogger<UsuariosController> logger,
        ListarUsuariosApiIntegration apiListarUsers)
        {
            _logger = logger;
            _apiListarUsers=apiListarUsers;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Usuario> users= await _apiListarUsers.GetAllUsuarios();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}