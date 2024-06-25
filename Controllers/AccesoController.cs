using _2daPracticaProgramada_OscarNaranjoZuniga.Models;
using _2daPracticaProgramada_OscarNaranjoZuniga.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            //Esto es por si uno ya esta autenticado y el tiempo de la sesion no ha caducado, entonces no aparecera el login de nuevo:
            if (User.Identity!.IsAuthenticated) 
            {
                return RedirectToAction("Index", "Listas");
            }
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

            // Crear lista de claims para guardar los datos del usuario que queremos que mantenga la sesion:
            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, Usuario_Recibido.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, Usuario_Recibido.Nombre),
                new Claim(ClaimTypes.Email, Usuario_Recibido.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true, };

            //Acá guardamos la informacion del usuario dentro de las cookies
            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            properties);

            return RedirectToAction("Index", "Listas");
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
    }
}