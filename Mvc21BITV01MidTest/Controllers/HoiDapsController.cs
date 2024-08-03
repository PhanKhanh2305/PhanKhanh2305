using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc21BITV01MidTest.Data;

namespace Mvc21BITV01MidTest.Controllers
{
    public class HoiDapsController : Controller
    {
        private readonly MyeStoreContext _context;

        public HoiDapsController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: HoiDaps
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.HoiDaps.Include(h => h.MaNvNavigation);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: HoiDaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoiDap = await _context.HoiDaps
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoiDap == null)
            {
                return NotFound();
            }

            return View(hoiDap);
        }

        // GET: HoiDaps/Create
        public IActionResult Create()
        {
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv");
            return View();
        }

        // POST: HoiDaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHd,CauHoi,TraLoi,NgayDua,MaNv")] HoiDap hoiDap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoiDap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoiDap.MaNv);
            return View(hoiDap);
        }

        // GET: HoiDaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoiDap = await _context.HoiDaps.FindAsync(id);
            if (hoiDap == null)
            {
                return NotFound();
            }
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoiDap.MaNv);
            return View(hoiDap);
        }

        // POST: HoiDaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHd,CauHoi,TraLoi,NgayDua,MaNv")] HoiDap hoiDap)
        {
            if (id != hoiDap.MaHd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoiDap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoiDapExists(hoiDap.MaHd))
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
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoiDap.MaNv);
            return View(hoiDap);
        }

        // GET: HoiDaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoiDap = await _context.HoiDaps
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoiDap == null)
            {
                return NotFound();
            }

            return View(hoiDap);
        }

        // POST: HoiDaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoiDap = await _context.HoiDaps.FindAsync(id);
            if (hoiDap != null)
            {
                _context.HoiDaps.Remove(hoiDap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoiDapExists(int id)
        {
            return _context.HoiDaps.Any(e => e.MaHd == id);
        }
    }
}
