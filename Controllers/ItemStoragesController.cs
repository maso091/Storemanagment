using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PINProjekt.Data;
using PINProjekt.Models;

namespace PINProjekt.Controllers
{
    [Authorize]
    public class ItemStoragesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemStoragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemStorages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemStorage.ToListAsync());
        }

        // GET: ItemStorages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemStorage = await _context.ItemStorage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemStorage == null)
            {
                return NotFound();
            }

            return View(itemStorage);
        }

        // GET: ItemStorages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemStorages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity")] ItemStorage itemStorage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemStorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemStorage);
        }

        // GET: ItemStorages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemStorage = await _context.ItemStorage.FindAsync(id);
            if (itemStorage == null)
            {
                return NotFound();
            }
            return View(itemStorage);
        }

        // POST: ItemStorages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity")] ItemStorage itemStorage)
        {
            if (id != itemStorage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemStorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemStorageExists(itemStorage.Id))
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
            return View(itemStorage);
        }

        // GET: ItemStorages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemStorage = await _context.ItemStorage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemStorage == null)
            {
                return NotFound();
            }

            return View(itemStorage);
        }

        // POST: ItemStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemStorage = await _context.ItemStorage.FindAsync(id);
            _context.ItemStorage.Remove(itemStorage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemStorageExists(int id)
        {
            return _context.ItemStorage.Any(e => e.Id == id);
        }
    }
}
