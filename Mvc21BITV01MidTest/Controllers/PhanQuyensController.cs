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
    public class PhanQuyensController : Controller
    {
        private readonly MyeStoreContext _context;

        public PhanQuyensController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: PhanQuyens
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.PhanQuyens.Include(p => p.MaPbNavigation).Include(p => p.MaTrangNavigation);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: PhanQuyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens
                .Include(p => p.MaPbNavigation)
                .Include(p => p.MaTrangNavigation)
                .FirstOrDefaultAsync(m => m.MaPq == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // GET: PhanQuyens/Create
        public IActionResult Create()
        {
            ViewData["MaPb"] = new SelectList(_context.PhongBans, "MaPb", "MaPb");
            ViewData["MaTrang"] = new SelectList(_context.TrangWebs, "MaTrang", "MaTrang");
            return View();
        }

        // POST: PhanQuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPq,MaPb,MaTrang,Them,Sua,Xoa,Xem")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phanQuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPb"] = new SelectList(_context.PhongBans, "MaPb", "MaPb", phanQuyen.MaPb);
            ViewData["MaTrang"] = new SelectList(_context.TrangWebs, "MaTrang", "MaTrang", phanQuyen.MaTrang);
            return View(phanQuyen);
        }

        // GET: PhanQuyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens.FindAsync(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            ViewData["MaPb"] = new SelectList(_context.PhongBans, "MaPb", "MaPb", phanQuyen.MaPb);
            ViewData["MaTrang"] = new SelectList(_context.TrangWebs, "MaTrang", "MaTrang", phanQuyen.MaTrang);
            return View(phanQuyen);
        }

        // POST: PhanQuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPq,MaPb,MaTrang,Them,Sua,Xoa,Xem")] PhanQuyen phanQuyen)
        {
            if (id != phanQuyen.MaPq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phanQuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanQuyenExists(phanQuyen.MaPq))
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
            ViewData["MaPb"] = new SelectList(_context.PhongBans, "MaPb", "MaPb", phanQuyen.MaPb);
            ViewData["MaTrang"] = new SelectList(_context.TrangWebs, "MaTrang", "MaTrang", phanQuyen.MaTrang);
            return View(phanQuyen);
        }

        // GET: PhanQuyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens
                .Include(p => p.MaPbNavigation)
                .Include(p => p.MaTrangNavigation)
                .FirstOrDefaultAsync(m => m.MaPq == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // POST: PhanQuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanQuyen = await _context.PhanQuyens.FindAsync(id);
            if (phanQuyen != null)
            {
                _context.PhanQuyens.Remove(phanQuyen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanQuyenExists(int id)
        {
            return _context.PhanQuyens.Any(e => e.MaPq == id);
        }
    }
}
