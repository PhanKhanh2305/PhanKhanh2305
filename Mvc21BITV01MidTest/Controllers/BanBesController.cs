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
    public class BanBesController : Controller
    {
        private readonly MyeStoreContext _context;

        public BanBesController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: BanBes
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.BanBes.Include(b => b.MaHhNavigation).Include(b => b.MaKhNavigation);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: BanBes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banBe = await _context.BanBes
                .Include(b => b.MaHhNavigation)
                .Include(b => b.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.MaBb == id);
            if (banBe == null)
            {
                return NotFound();
            }

            return View(banBe);
        }

        // GET: BanBes/Create
        public IActionResult Create()
        {
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh");
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            return View();
        }

        // POST: BanBes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBb,MaKh,MaHh,HoTen,Email,NgayGui,GhiChu")] BanBe banBe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banBe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", banBe.MaHh);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", banBe.MaKh);
            return View(banBe);
        }

        // GET: BanBes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banBe = await _context.BanBes.FindAsync(id);
            if (banBe == null)
            {
                return NotFound();
            }
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", banBe.MaHh);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", banBe.MaKh);
            return View(banBe);
        }

        // POST: BanBes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBb,MaKh,MaHh,HoTen,Email,NgayGui,GhiChu")] BanBe banBe)
        {
            if (id != banBe.MaBb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banBe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BanBeExists(banBe.MaBb))
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
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", banBe.MaHh);
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", banBe.MaKh);
            return View(banBe);
        }

        // GET: BanBes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banBe = await _context.BanBes
                .Include(b => b.MaHhNavigation)
                .Include(b => b.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.MaBb == id);
            if (banBe == null)
            {
                return NotFound();
            }

            return View(banBe);
        }

        // POST: BanBes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banBe = await _context.BanBes.FindAsync(id);
            if (banBe != null)
            {
                _context.BanBes.Remove(banBe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BanBeExists(int id)
        {
            return _context.BanBes.Any(e => e.MaBb == id);
        }
    }
}
