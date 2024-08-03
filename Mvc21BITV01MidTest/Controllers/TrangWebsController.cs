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
    public class TrangWebsController : Controller
    {
        private readonly MyeStoreContext _context;

        public TrangWebsController(MyeStoreContext context)
        {
            _context = context;
        }

        // GET: TrangWebs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrangWebs.ToListAsync());
        }

        // GET: TrangWebs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangWeb = await _context.TrangWebs
                .FirstOrDefaultAsync(m => m.MaTrang == id);
            if (trangWeb == null)
            {
                return NotFound();
            }

            return View(trangWeb);
        }

        // GET: TrangWebs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrangWebs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrang,TenTrang,Url")] TrangWeb trangWeb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangWeb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangWeb);
        }

        // GET: TrangWebs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangWeb = await _context.TrangWebs.FindAsync(id);
            if (trangWeb == null)
            {
                return NotFound();
            }
            return View(trangWeb);
        }

        // POST: TrangWebs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrang,TenTrang,Url")] TrangWeb trangWeb)
        {
            if (id != trangWeb.MaTrang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangWeb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangWebExists(trangWeb.MaTrang))
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
            return View(trangWeb);
        }

        // GET: TrangWebs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangWeb = await _context.TrangWebs
                .FirstOrDefaultAsync(m => m.MaTrang == id);
            if (trangWeb == null)
            {
                return NotFound();
            }

            return View(trangWeb);
        }

        // POST: TrangWebs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangWeb = await _context.TrangWebs.FindAsync(id);
            if (trangWeb != null)
            {
                _context.TrangWebs.Remove(trangWeb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangWebExists(int id)
        {
            return _context.TrangWebs.Any(e => e.MaTrang == id);
        }
    }
}
