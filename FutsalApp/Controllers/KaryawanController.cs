using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutsalApp.Data;
using FutsalApp.Models;

namespace FutsalApp.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly FutsalAppContext _context;

        public KaryawanController(FutsalAppContext context)
        {
            _context = context;
        }

        // GET: Karyawan
        public async Task<IActionResult> Index()
        {
              return _context.Karyawan != null ? 
                          View(await _context.Karyawan.ToListAsync()) :
                          Problem("Entity set 'FutsalAppContext.Karyawan'  is null.");
        }

        // GET: Karyawan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Karyawan == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // GET: Karyawan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karyawan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama,Alamat,NomorHP")] Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karyawan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karyawan);
        }

        // GET: Karyawan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Karyawan == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan.FindAsync(id);
            if (karyawan == null)
            {
                return NotFound();
            }
            return View(karyawan);
        }

        // POST: Karyawan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,Alamat,NomorHP")] Karyawan karyawan)
        {
            if (id != karyawan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karyawan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KaryawanExists(karyawan.Id))
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
            return View(karyawan);
        }

        // GET: Karyawan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Karyawan == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // POST: Karyawan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Karyawan == null)
            {
                return Problem("Entity set 'FutsalAppContext.Karyawan'  is null.");
            }
            var karyawan = await _context.Karyawan.FindAsync(id);
            if (karyawan != null)
            {
                _context.Karyawan.Remove(karyawan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KaryawanExists(int id)
        {
          return (_context.Karyawan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
