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
    public class PrivaciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrivaciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Privacies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Privacy.ToListAsync());
        }

        // GET: Privacies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privacy = await _context.Privacy
                .FirstOrDefaultAsync(m => m.ID == id);
            if (privacy == null)
            {
                return NotFound();
            }

            return View(privacy);
        }

        // GET: Privacies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Privacies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] Privacy privacy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(privacy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(privacy);
        }

        // GET: Privacies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privacy = await _context.Privacy.FindAsync(id);
            if (privacy == null)
            {
                return NotFound();
            }
            return View(privacy);
        }

        // POST: Privacies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Privacy privacy)
        {
            if (id != privacy.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privacy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivacyExists(privacy.ID))
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
            return View(privacy);
        }

        // GET: Privacies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privacy = await _context.Privacy
                .FirstOrDefaultAsync(m => m.ID == id);
            if (privacy == null)
            {
                return NotFound();
            }

            return View(privacy);
        }

        // POST: Privacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var privacy = await _context.Privacy.FindAsync(id);
            if (privacy != null)
            {
                _context.Privacy.Remove(privacy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrivacyExists(int id)
        {
            return _context.Privacy.Any(e => e.ID == id);
        }
    }
}
