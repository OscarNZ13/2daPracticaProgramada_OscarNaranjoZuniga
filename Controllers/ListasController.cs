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
            var applicationDbContext = _context.Listas.Include(l => l.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Listas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Listas
                .Include(l => l.Usuario)
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Listas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListaId,UsuarioId,Titulo,FechaCreacion")] Lista lista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", lista.UsuarioId);
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", lista.UsuarioId);
            return View(lista);
        }

        // POST: Listas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListaId,UsuarioId,Titulo,FechaCreacion")] Lista lista)
        {
            if (id != lista.ListaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaExists(lista.ListaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", lista.UsuarioId);
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
                .Include(l => l.Usuario)
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
