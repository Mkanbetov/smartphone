using Microsoft.AspNetCore.Mvc;
using Smartphone.Data;
using Smartphone.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Smartphone.Controllers
{
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> _brands;

        public BrandController(IRepository<Brand> brands)
        {
            _brands = brands;
        }

        // GET: /Brand
        public async Task<IActionResult> Index()
        {
            var brands = await _brands.GetAllAsync();
            return View(brands);
        }

        // GET: /Brand/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Brand/Create
        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            // 🔴 ВАЖНО: если false — форма передаёт данные неправильно
            

            await _brands.AddAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Brand/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = (await _brands.GetAllAsync())
                .FirstOrDefault(b => b.Id == id);

            if (brand == null)
                return NotFound();

            return View(brand);
        }

        // POST: /Brand/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            await _brands.UpdateAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Brand/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _brands.GetByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _brands.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: /Brand/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id, [FromServices] IRepository<Product> products)
        {
            var brand = await _brands.GetByIdAsync(id);
            if (brand == null) return NotFound();

            // Получаем продукты этого бренда
            var brandProducts = (await products.GetAllAsync())
                                .Where(p => p.BrandId == id)
                                .ToList();

            // Передаём в View через ViewBag
            ViewBag.Brand = brand;
            ViewBag.Products = brandProducts;

            return View();
        }

    }
}


