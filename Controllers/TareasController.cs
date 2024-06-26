using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2daPracticaProgramada_OscarNaranjoZuniga.Models;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Controllers
{
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index(int listaId)
        {
            ViewBag.ListaId = listaId;
            // Filtrar las tareas por el ID de la lista
            var tareas = await _context.Tareas
                                       .Where(t => t.ListaId == listaId)
                                       .Include(t => t.Lista)
                                       .ToListAsync();
            
            return View(tareas);
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Lista)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create(int listaId)
        {
            ViewBag.ListaId = listaId;
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListaId,Titulo,Descripcion")] Tarea tarea)
        {
            ViewBag.ListaId = tarea.ListaId;
            _context.Add(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { listaId = tarea.ListaId });
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["ListaId"] = new SelectList(_context.Listas, "ListaId", "ListaId", tarea.ListaId);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaId,ListaId,Titulo,Descripcion,Completada")] Tarea tarea)
        {
            if (id != tarea.TareaId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { listaId = tarea.ListaId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(tarea.TareaId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
        }



        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Lista)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.TareaId == id);
        }
    }
}
