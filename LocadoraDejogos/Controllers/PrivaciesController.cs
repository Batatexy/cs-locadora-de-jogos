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
        public async Task<IActionResult> Details()
        {
            return View(await _context.Privacy.ToListAsync());
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
            return View(await _context.Privacy.ToListAsync());
        }

        // GET: Privacies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _context.Privacy.ToListAsync());
        }

        // POST: Privacies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Privacy privacy)
        {
            return View(await _context.Privacy.ToListAsync());
        }

        // GET: Privacies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await _context.Privacy.ToListAsync());
        }

        // POST: Privacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View(await _context.Privacy.ToListAsync());
        }

        private bool PrivacyExists(int id)
        {
            return _context.Privacy.Any(e => e.ID == id);
        }
    }
}
