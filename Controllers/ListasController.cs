using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2daPracticaProgramada_OscarNaranjoZuniga.Models;
using System.Security.Claims;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Controllers
{
    public class ListasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Listas
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Listas.ToListAsync());

            // Obtener el ID del usuario logeado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Verificar si el userId no es nulo
            if (userId == null)
            {
                return Unauthorized(); // O redirigir a la página de login
            }

            // Convertir el userId a int
            int userIdInt = int.Parse(userId);

            // Filtrar las listas por el ID del usuario logeado
            var listas = await _context.Listas
                                       .Where(l => l.UsuarioId == userIdInt)
                                       .ToListAsync();

            return View(listas);
        }

        // GET: Listas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Listas
                .FirstOrDefaultAsync(m => m.ListaId == id);
            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // GET: Listas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Listas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Titulo")] Lista lista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        // GET: Listas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Listas.FindAsync(id);
            if (lista == null)
            {
                return NotFound();
            }
            return View(lista);
        }

        // POST: Listas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListaId,Titulo")] Lista lista)
        {

            Lista? Lista_Recibida = await _context.Listas.Where(l => l.ListaId == id).FirstOrDefaultAsync();

            if (Lista_Recibida == null)
            {
                ViewData["Mensaje"] = "Error al editar o encontrar la lista";
                return View();
            }

            // Actualizar los campos necesarios
            Lista_Recibida.Titulo = lista.Titulo;

            if (ModelState.IsValid)
            {
                try
                {
                    // Guardar los cambios en la base de datos
                    _context.Update(Lista_Recibida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Manejar la excepción de concurrencia
                    ViewData["Mensaje"] = "Error de concurrencia al intentar guardar los cambios. Inténtalo de nuevo.";
                    
                }
                catch (DbUpdateException ex)
                {
                    // Manejar otras excepciones de actualización
                    ViewData["Mensaje"] = $"Error al actualizar la lista: {ex.Message}";
                    return View(lista);
                }

                // Redirigir a la acción Index del controlador Listas
                return RedirectToAction("Index", "Listas");
            }

            return View(lista);
        }

        // GET: Listas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Listas
                .FirstOrDefaultAsync(m => m.ListaId == id);
            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // POST: Listas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lista = await _context.Listas.FindAsync(id);
            if (lista != null)
            {
                _context.Listas.Remove(lista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaExists(int id)
        {
            return _context.Listas.Any(e => e.ListaId == id);
        }
    }
}
