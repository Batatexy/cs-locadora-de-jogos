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
    public class ConsolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consoles.ToListAsync());
        }

        // GET: Consoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consoles = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consoles == null)
            {
                return NotFound();
            }

            return View(consoles);
        }

        // GET: Consoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Fabricante,Geracao,Ano")] Consoles consoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consoles);
        }

        // GET: Consoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consoles = await _context.Consoles.FindAsync(id);
            if (consoles == null)
            {
                return NotFound();
            }
            return View(consoles);
        }

        // POST: Consoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Fabricante,Geracao,Ano")] Consoles consoles)
        {
            if (id != consoles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsolesExists(consoles.ID))
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
            return View(consoles);
        }

        // GET: Consoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consoles = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consoles == null)
            {
                return NotFound();
            }

            return View(consoles);
        }

        // POST: Consoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consoles = await _context.Consoles.FindAsync(id);
            if (consoles != null)
            {
                _context.Consoles.Remove(consoles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsolesExists(int id)
        {
            return _context.Consoles.Any(e => e.ID == id);
        }
    }
}
