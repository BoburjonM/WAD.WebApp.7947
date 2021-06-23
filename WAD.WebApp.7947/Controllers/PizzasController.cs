using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAD.WebApp._7947.DAL;
using WAD.WebApp._7947.DAL.DTO;

namespace WAD.WebApp._7947.Controllers
{
    public class PizzasController : Controller
    {
        private readonly PizzaStoreDbContext _context;

        public PizzasController(PizzaStoreDbContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            var pizzaStoreDbContext = _context.Pizza.Include(p => p.Category);
            return View(await pizzaStoreDbContext.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            ViewData["Size"] = new SelectList(Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>().ToList());
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PizzaId,PizzaName,Price,CategoryId,Size,PizzaPhoto")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                byte[] photoBytes = null;
                if (pizza.PizzaPhoto != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        pizza.PizzaPhoto.CopyTo(memory);
                        photoBytes = memory.ToArray();
                    }
                }
                pizza.PizzaBinPhoto = photoBytes;
                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Size"] = new SelectList(Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>().ToList());
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", pizza.CategoryId);
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            ViewData["Size"] = new SelectList(Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>().ToList());
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", pizza.CategoryId);
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PizzaId,PizzaName,Price,CategoryId,Size,PizzaBinPhoto")] Pizza pizza)
        {
            if (id != pizza.PizzaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] photoBytes = null;
                    if (pizza.PizzaPhoto != null)
                    {
                        using (var memory = new MemoryStream())
                        {
                            pizza.PizzaPhoto.CopyTo(memory);
                            photoBytes = memory.ToArray();
                        }
                    }
                    else
                    {
                        photoBytes = pizza.PizzaBinPhoto;
                    }
                    pizza.PizzaBinPhoto = photoBytes;
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.PizzaId))
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
            ViewData["Size"] = new SelectList(Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>().ToList());
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", pizza.CategoryId);
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.PizzaId == id);
        }
        public async Task<IActionResult> ShowImage(int? id)
        {
            if (id.HasValue)
            {
                var pizza = await _context.Pizza.FindAsync(id);
                if (pizza?.PizzaBinPhoto != null)
                {
                    return File(
                        pizza.PizzaBinPhoto,
                        "image/jpeg",
                        $"pizza_{id}.jpg");
                }
            }

            return NotFound();
        }
    }
}
