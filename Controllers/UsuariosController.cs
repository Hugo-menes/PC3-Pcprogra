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
        private readonly ListarUsuarioApiIntegration _apiUser;
        private readonly CrearUsuarioApiIntegration _crearUser;


        public UsuariosController(ILogger<UsuariosController> logger,
        ListarUsuariosApiIntegration apiListarUsers,
        ListarUsuarioApiIntegration apiUser,
        CrearUsuarioApiIntegration crearUser)
        {
            _logger = logger;
            _apiListarUsers=apiListarUsers;
            _apiUser =apiUser;
            _crearUser=crearUser;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Usuario> users= await _apiListarUsers.GetAllUsuarios();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Inicio(int Id)
        {
            Usuario usuario= await _apiUser.GetUsuario(Id);
            return View(usuario);
        }
        
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(string name, string job)
        {
            try
            {
                var response = await _crearUser.CrearUsuario(name, job);
                
                if (response != null)
                {
                    TempData["SuccessMessage"] = "Usuario creado correctamente.";
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear el usuario");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el usuario");
            }
            
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}