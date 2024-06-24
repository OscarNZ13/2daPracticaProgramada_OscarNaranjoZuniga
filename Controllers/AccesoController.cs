using _2daPracticaProgramada_OscarNaranjoZuniga.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;

        public AccesoController(ApplicationDbContext AppDbContext) 
        {
            _appDbContext = AppDbContext;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario Usuario)
        {
            Usuario User = new Usuario() 
            {Nombre = Usuario.Nombre,
            Email = Usuario.Email,
            Contrasena = Usuario.Contrasena};

            await _appDbContext.Usuarios.AddAsync(Usuario);
            await _appDbContext.SaveChangesAsync();

            // SI se crea el ususario el id seria diferente de 0, por lo cual se habria registrado
            if (Usuario.UsuarioId != 0)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else 
            {
                ViewData["Mensaje"]= "No sé pudo crear el usuario";
                return View();
            }
        }
    }
}
