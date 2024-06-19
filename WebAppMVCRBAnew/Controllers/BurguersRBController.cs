using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVCRBAnew.Models;
using WebAppMVCRBAnew.Data;

namespace WebAppMVCRBAnew.Controllers
{
    public class BurguersRBController : Controller
    {
        private readonly WebAppMVCRBAnewContext _context;

        public BurguersRBController(WebAppMVCRBAnewContext context)
        {
            _context = context;
        }

        // GET: BurguersRB
        public async Task<IActionResult> Index()
        {
            return View(await _context.Burguer.ToListAsync());
        }

        // GET: BurguersRB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burguer = await _context.Burguer
                .FirstOrDefaultAsync(m => m.BurguerId == id);
            if (burguer == null)
            {
                return NotFound();
            }

            return View(burguer);
        }

        // GET: BurguersRB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BurguersRB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BurguerId,Name,WithCheese,Precio")] Burguer burguer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burguer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burguer);
        }

        // GET: BurguersRB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burguer = await _context.Burguer.FindAsync(id);
            if (burguer == null)
            {
                return NotFound();
            }
            return View(burguer);
        }

        // POST: BurguersRB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BurguerId,Name,WithCheese,Precio")] Burguer burguer)
        {
            if (id != burguer.BurguerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burguer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurguerExists(burguer.BurguerId))
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
            return View(burguer);
        }

        // GET: BurguersRB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burguer = await _context.Burguer
                .FirstOrDefaultAsync(m => m.BurguerId == id);
            if (burguer == null)
            {
                return NotFound();
            }

            return View(burguer);
        }

        // POST: BurguersRB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burguer = await _context.Burguer.FindAsync(id);
            if (burguer != null)
            {
                _context.Burguer.Remove(burguer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurguerExists(int id)
        {
            return _context.Burguer.Any(e => e.BurguerId == id);
        }
    }
}
