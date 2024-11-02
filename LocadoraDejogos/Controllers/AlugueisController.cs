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
    public class AlugueisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlugueisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alugueis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Alugueis.Include(a => a.Clientes).Include(a => a.Funcionarios).Include(a => a.Jogos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alugueis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis
                .Include(a => a.Clientes)
                .Include(a => a.Funcionarios)
                .Include(a => a.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (alugueis == null)
            {
                return NotFound();
            }

            return View(alugueis);
        }

        // GET: Alugueis/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID");
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "ID");
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID");
            return View();
        }

        // POST: Alugueis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClienteID,JogoID,FuncionarioID")] Alugueis alugueis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alugueis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "ID", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", alugueis.JogoID);
            return View(alugueis);
        }

        // GET: Alugueis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis.FindAsync(id);
            if (alugueis == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "ID", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", alugueis.JogoID);
            return View(alugueis);
        }

        // POST: Alugueis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClienteID,JogoID,FuncionarioID")] Alugueis alugueis)
        {
            if (id != alugueis.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alugueis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlugueisExists(alugueis.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "ID", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "ID", alugueis.JogoID);
            return View(alugueis);
        }

        // GET: Alugueis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis
                .Include(a => a.Clientes)
                .Include(a => a.Funcionarios)
                .Include(a => a.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (alugueis == null)
            {
                return NotFound();
            }

            return View(alugueis);
        }

        // POST: Alugueis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alugueis = await _context.Alugueis.FindAsync(id);
            if (alugueis != null)
            {
                _context.Alugueis.Remove(alugueis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlugueisExists(int id)
        {
            return _context.Alugueis.Any(e => e.ID == id);
        }
    }
}
