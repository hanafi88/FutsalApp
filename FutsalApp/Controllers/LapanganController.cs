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
    public class LapanganController : Controller
    {
        private readonly FutsalAppContext _context;

        public LapanganController(FutsalAppContext context)
        {
            _context = context;
        }

        // GET: Lapangans
        public async Task<IActionResult> Index()
        {
            if (_context.Lapangan != null)
            {
                List<Lapangan> lstLapangan = await _context.Lapangan.ToListAsync();
                List<Karyawan> lstKarywan = await _context.Karyawan.ToListAsync();

                foreach (Lapangan obj in lstLapangan)
                {
                    obj.Karyawan = lstKarywan.FirstOrDefault(x => x != null && x.Id == obj.KaryawanId).Nama;
                }

                return View(lstLapangan);
            }

            return Problem("Entity set 'FutsalAppContext.Lapangan'  is null.");
        }

        // GET: Lapangans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lapangan == null)
            {
                return NotFound();
            }

            var lapangan = await _context.Lapangan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lapangan == null)
            {
                return NotFound();
            }

            List<Karyawan> lstKarywan = await _context.Karyawan.ToListAsync();
            lapangan.Karyawan = lstKarywan.FirstOrDefault(x => x != null && x.Id == lapangan.KaryawanId).Nama;

            return View(lapangan);
        }

        // GET: Lapangans/Create
        public IActionResult Create()
        {
            var lapangan = new Lapangan();
            List<Karyawan> karyawans = new List<Karyawan>();

            karyawans.Add(new Karyawan() { Id = -1, Nama = "Pilih Karyawan" });
            karyawans.AddRange(_context.Karyawan.Distinct().ToList());

            lapangan.Karyawans = new SelectList(karyawans, "Id", "Nama");

            return View(lapangan);
        }

        // POST: Lapangans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kode,Nama,Alamat,KaryawanId")] Lapangan lapangan)
        {
            ModelState.Remove("Karyawan");
            ModelState.Remove("Karyawans");

            if (ModelState.IsValid)
            {
                _context.Add(lapangan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lapangan);
        }

        // GET: Lapangans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lapangan == null)
            {
                return NotFound();
            }

            var lapangan = await _context.Lapangan.FindAsync(id);
            if (lapangan == null)
            {
                return NotFound();
            }
            
            var karyawans = _context.Karyawan.Distinct().ToList();
            lapangan.Karyawans = new SelectList(karyawans, "Id", "Nama", "KaryawanId");

            return View(lapangan);
        }

        // POST: Lapangans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kode,Nama,Alamat,KaryawanId")] Lapangan lapangan)
        {
            if (id != lapangan.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Karyawan");
            ModelState.Remove("Karyawans");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lapangan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LapanganExists(lapangan.Id))
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
            return View(lapangan);
        }

        // GET: Lapangans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lapangan == null)
            {
                return NotFound();
            }

            var lapangan = await _context.Lapangan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lapangan == null)
            {
                return NotFound();
            }

            return View(lapangan);
        }

        // POST: Lapangans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lapangan == null)
            {
                return Problem("Entity set 'FutsalAppContext.Lapangan'  is null.");
            }
            var lapangan = await _context.Lapangan.FindAsync(id);
            if (lapangan != null)
            {
                _context.Lapangan.Remove(lapangan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LapanganExists(int id)
        {
            return (_context.Lapangan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
