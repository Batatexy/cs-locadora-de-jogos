using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraDejogos.Data;
using LocadoraDejogos.Models;

namespace LocadoraDejogos.Controllers
{
    public class JogosConsolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogosConsolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JogosConsoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JogosConsoles.Include(j => j.Consoles).Include(j => j.Jogos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JogosConsoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogosConsoles = await _context.JogosConsoles
                .Include(j => j.Consoles)
                .Include(j => j.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jogosConsoles == null)
            {
                return NotFound();
            }

            return View(jogosConsoles);
        }

        // GET: JogosConsoles/Create
        public IActionResult Create()
        {
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID");
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID");
            return View();
        }

        // POST: JogosConsoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JogoID,ConsoleID")] JogosConsoles jogosConsoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogosConsoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogosConsoles.ConsoleID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", jogosConsoles.JogoID);
            return View(jogosConsoles);
        }

        // GET: JogosConsoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogosConsoles = await _context.JogosConsoles.FindAsync(id);
            if (jogosConsoles == null)
            {
                return NotFound();
            }
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogosConsoles.ConsoleID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", jogosConsoles.JogoID);
            return View(jogosConsoles);
        }

        // POST: JogosConsoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JogoID,ConsoleID")] JogosConsoles jogosConsoles)
        {
            if (id != jogosConsoles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogosConsoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogosConsolesExists(jogosConsoles.ID))
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
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogosConsoles.ConsoleID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", jogosConsoles.JogoID);
            return View(jogosConsoles);
        }

        // GET: JogosConsoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogosConsoles = await _context.JogosConsoles
                .Include(j => j.Consoles)
                .Include(j => j.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jogosConsoles == null)
            {
                return NotFound();
            }

            return View(jogosConsoles);
        }

        // POST: JogosConsoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogosConsoles = await _context.JogosConsoles.FindAsync(id);
            if (jogosConsoles != null)
            {
                _context.JogosConsoles.Remove(jogosConsoles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogosConsolesExists(int id)
        {
            return _context.JogosConsoles.Any(e => e.ID == id);
        }
    }
}
