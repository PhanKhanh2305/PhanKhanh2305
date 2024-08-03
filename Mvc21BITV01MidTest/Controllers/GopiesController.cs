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
    public class GopiesController : Controller
    {
        private readonly MyeStoreContext _context;

        public GopiesController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: Gopies
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.Gopies.Include(g => g.MaCdNavigation);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: Gopies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies
                .Include(g => g.MaCdNavigation)
                .FirstOrDefaultAsync(m => m.MaGy == id);
            if (gopY == null)
            {
                return NotFound();
            }

            return View(gopY);
        }

        // GET: Gopies/Create
        public IActionResult Create()
        {
            ViewData["MaCd"] = new SelectList(_context.ChuDes, "MaCd", "MaCd");
            return View();
        }

        // POST: Gopies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGy,MaCd,NoiDung,NgayGy,HoTen,Email,DienThoai,CanTraLoi,TraLoi,NgayTl")] GopY gopY)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gopY);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCd"] = new SelectList(_context.ChuDes, "MaCd", "MaCd", gopY.MaCd);
            return View(gopY);
        }

        // GET: Gopies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies.FindAsync(id);
            if (gopY == null)
            {
                return NotFound();
            }
            ViewData["MaCd"] = new SelectList(_context.ChuDes, "MaCd", "MaCd", gopY.MaCd);
            return View(gopY);
        }

        // POST: Gopies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGy,MaCd,NoiDung,NgayGy,HoTen,Email,DienThoai,CanTraLoi,TraLoi,NgayTl")] GopY gopY)
        {
            if (id != gopY.MaGy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gopY);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GopYExists(gopY.MaGy))
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
            ViewData["MaCd"] = new SelectList(_context.ChuDes, "MaCd", "MaCd", gopY.MaCd);
            return View(gopY);
        }

        // GET: Gopies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies
                .Include(g => g.MaCdNavigation)
                .FirstOrDefaultAsync(m => m.MaGy == id);
            if (gopY == null)
            {
                return NotFound();
            }

            return View(gopY);
        }

        // POST: Gopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gopY = await _context.Gopies.FindAsync(id);
            if (gopY != null)
            {
                _context.Gopies.Remove(gopY);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GopYExists(string id)
        {
            return _context.Gopies.Any(e => e.MaGy == id);
        }
    }
}
