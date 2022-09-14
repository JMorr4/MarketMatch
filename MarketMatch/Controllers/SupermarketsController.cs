using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketMatch.Models;
using MarketMatch.Data;
using MarketMatch.Models;

namespace MarketMatch.Controllers
{
    public class SupermarketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SupermarketsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env; // We need this to access the web root path (WebRootPath)
        }

        // GET: Supermarkets
        public async Task<IActionResult> Index()
        {
              return _context.Supermarket != null ? 
                          View(await _context.Supermarket.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Supermarket'  is null.");
        }

        // GET: Supermarkets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supermarket == null)
            {
                return NotFound();
            }

            var supermarket = await _context.Supermarket
                .FirstOrDefaultAsync(m => m.SupermarketId == id);
            if (supermarket == null)
            {
                return NotFound();
            }

            return View(supermarket);
        }

        // GET: Supermarkets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supermarkets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupermarketId,Name,Logo,LogoFile")] Supermarket supermarket)
        {
            // Ignore invalid status of data pointed to by the foreign key as not relevant here
            ModelState.Remove(nameof(Supermarket.ProductPrices));

            await UploadSupermarketFile(supermarket);

            if (ModelState.IsValid)
            {
                _context.Add(supermarket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supermarket);
        }

        // GET: Supermarkets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supermarket == null)
            {
                return NotFound();
            }

            var supermarket = await _context.Supermarket.FindAsync(id);
            if (supermarket == null)
            {
                return NotFound();
            }
            return View(supermarket);
        }

        // POST: Supermarkets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupermarketId,Name,Logo,LogoFile")] Supermarket supermarket)
        {
            if (id != supermarket.SupermarketId)
            {
                return NotFound();
            }

            // Ignore invalid status of data pointed to by the foreign key as not relevant here
            ModelState.Remove(nameof(Supermarket.ProductPrices));

            await UploadSupermarketFile(supermarket);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supermarket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupermarketExists(supermarket.SupermarketId))
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
            return View(supermarket);
        }

        // GET: Supermarkets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supermarket == null)
            {
                return NotFound();
            }

            var supermarket = await _context.Supermarket
                .FirstOrDefaultAsync(m => m.SupermarketId == id);
            if (supermarket == null)
            {
                return NotFound();
            }

            return View(supermarket);
        }

        // POST: Supermarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supermarket == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Supermarket'  is null.");
            }
            var supermarket = await _context.Supermarket.FindAsync(id);
            if (supermarket != null)
            {
                _context.Supermarket.Remove(supermarket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupermarketExists(int id)
        {
          return (_context.Supermarket?.Any(e => e.SupermarketId == id)).GetValueOrDefault();
        }

        // Function to copy the uploaded file to the webserver 
        private async Task<Boolean> UploadSupermarketFile(Supermarket supermarket)
        {
            if (supermarket != null)
            {
                // Extract filename from passed to save in the db
                supermarket.Logo = supermarket.LogoFile.FileName;

                // Mark as valid
                ModelState.ClearValidationState("Logo");
                ModelState.MarkFieldValid("Logo");

                // check if any file is uploaded
                var work = supermarket.LogoFile;

                if (work != null)
                {
                    var filePath = Path.Combine(_env.WebRootPath, "images", supermarket.LogoFile.FileName);

                    // open-create the file in a stream and copy the uploaded file content into 
                    // the new file (IFormFile contains a stream) 
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await work.CopyToAsync(fileSteam);
                    }
                }
            }
            return true;
        }

    }
}
