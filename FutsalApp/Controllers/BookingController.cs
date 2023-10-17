using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutsalApp.Data;
using FutsalApp.Models;
//using NuGet.Packaging;

namespace FutsalApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly FutsalAppContext _context;

        public BookingController(FutsalAppContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            if (_context.Booking != null)
            {
                List<Booking> lstBookings = await _context.Booking.ToListAsync();
                List<Member> lstMembers = await _context.Member.ToListAsync();
                List<Karyawan> lstKaryawans = await _context.Karyawan.ToListAsync();
                List<Lapangan> lstLapangans = await _context.Lapangan.ToListAsync();

                foreach (Booking booking in lstBookings)
                {
                    booking.Member = lstMembers.FirstOrDefault(x => x != null && x.Id == booking.MemberId).Nama;
                    booking.Karyawan = lstKaryawans.FirstOrDefault(x => x != null && x.Id == booking.KaryawanId).Nama;
                    booking.Lapangan = lstLapangans.FirstOrDefault(x => x != null && x.Id == booking.LapanganId).Nama;
                }

                return View(lstBookings);
            }

            return Problem("Entity set 'FutsalAppContext.Booking'  is null.");
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            List<Member> lstMembers = await _context.Member.ToListAsync();
            List<Karyawan> lstKaryawans = await _context.Karyawan.ToListAsync();
            List<Lapangan> lstLapangans = await _context.Lapangan.ToListAsync();

            booking.Member = lstMembers.FirstOrDefault(x => x != null && x.Id == booking.MemberId).Nama;
            booking.Karyawan = lstKaryawans.FirstOrDefault(x => x != null && x.Id == booking.KaryawanId).Nama;
            booking.Lapangan = lstLapangans.FirstOrDefault(x => x != null && x.Id == booking.LapanganId).Nama;

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            var booking = new Booking();
            List<Member> members = new List<Member>();
            List<Karyawan> karyawans = new List<Karyawan>();
            List<Lapangan> lapangans = new List<Lapangan>();

            members.Add(new Member() { Id = -1, Nama = "Pilih Member" });
            members.AddRange(_context.Member.Distinct().ToList());

            karyawans.Add(new Karyawan() { Id = -1, Nama = "Pilih Karyawan" });
            karyawans.AddRange(_context.Karyawan.Distinct().ToList());

            lapangans.Add(new Lapangan() { Id = -1, Nama = "Pilih Lapangan" });
            lapangans.AddRange(_context.Lapangan.Distinct().ToList());

            booking.Members = new SelectList(members, "Id", "Nama");
            booking.Karyawans = new SelectList(karyawans, "Id", "Nama");
            booking.Lapangans = new SelectList(lapangans, "Id", "Nama");

            return View(booking);
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,KaryawanId,LapanganId,TanggalBooking,DurasiBooking")] Booking booking)
        {
            ModelState.Remove("Members");
            ModelState.Remove("Karyawans");
            ModelState.Remove("Lapangans");
            ModelState.Remove("Member");
            ModelState.Remove("Karyawan");
            ModelState.Remove("Lapangan");

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var members = _context.Member.Distinct().ToList();
            var karyawans = _context.Karyawan.Distinct().ToList();
            var lapangans = _context.Lapangan.Distinct().ToList();

            booking.Members = new SelectList(members, "Id", "Nama", "MemberId");
            booking.Karyawans = new SelectList(karyawans, "Id", "Nama", "KaryawanId");
            booking.Lapangans = new SelectList(lapangans, "Id", "Nama", "LapanganId");

            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,KaryawanId,LapanganId,TanggalBooking,DurasiBooking")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Members");
            ModelState.Remove("Karyawans");
            ModelState.Remove("Lapangans");
            ModelState.Remove("Member");
            ModelState.Remove("Karyawan");
            ModelState.Remove("Lapangan");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'FutsalAppContext.Booking'  is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
