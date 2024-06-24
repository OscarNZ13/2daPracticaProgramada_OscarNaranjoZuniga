using _2daPracticaProgramada_OscarNaranjoZuniga.Models;
using _2daPracticaProgramada_OscarNaranjoZuniga.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Controllers
{
    public class AccesoController : Controller
    {
        //Con esta variable del nuestro contexto es la que se va a utilizar para realizar acciones con la DB
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
            //Se crea el objeto con la informacion del post
            Usuario User = new Usuario()
            {
                Nombre = Usuario.Nombre,
                Email = Usuario.Email,
                Contrasena = Usuario.Contrasena
            };

            //Se envia a la db
            await _appDbContext.Usuarios.AddAsync(Usuario);
            await _appDbContext.SaveChangesAsync();

            // Si se crea el ususario el id seria diferente de 0, por lo cual se habria registrado
            if (Usuario.UsuarioId != 0)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewData["Mensaje"] = "No sé pudo crear el usuario";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM Usuario)
        {
            Usuario? Usuario_Recibido = await _appDbContext.Usuarios.Where(u => u.Email == Usuario.Email 
                                                                            && u.Contrasena == Usuario.Contrasena)      
                                                                            .FirstOrDefaultAsync(); // Si lo encuentra me lo devuelve, sino devuelve un null

            //Aqui validamos que nos ha devuelto la consulta
            if(Usuario_Recibido == null)
            {
                ViewData["Mensaje"] = "Error al encontrar el usuario";
                return View();
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
