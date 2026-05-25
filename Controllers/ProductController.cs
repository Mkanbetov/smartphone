using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartphone.Models;
using Smartphone.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smartphone.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> products;
        private IRepository<Brand> brands;
        private IRepository<Category> categories;


        public ProductController(IRepository<Product> productRepo,
            IRepository<Brand> brandRepo,
            IRepository<Category> categoryRepo)
        {
            products = productRepo;
            brands = brandRepo;
            categories = categoryRepo;

        }

        // GET: /Product
        public async Task<IActionResult> Index()
        {
            var options = new QueryOptions<Product>();
            var allProducts = await products.GetAllAsync(options);
            return View(allProducts);
        }
        // GET: /Product/Delete/5
        // GET: /Product/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = (await products.GetAllAsync(new QueryOptions<Product> { Includes = new[] { "Brand", "Category" } }))
                            .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Product product)
        {
            await products.DeleteAsync(product.Id);
            return RedirectToAction("Index");
        }
        // GET: /Product/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = (await products.GetAllAsync(
                new QueryOptions<Product>
                {
                    Includes = new[] { "Brand", "Category" }
                }))
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
        // GET: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await brands.GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await categories.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await products.AddAsync(product);
                return RedirectToAction("Index");
            }

            // если ошибка — заново заполняем списки
            ViewBag.Brands = new SelectList(await brands.GetAllAsync(), "Id", "Name", product.BrandId);
            ViewBag.Categories = new SelectList(await categories.GetAllAsync(), "Id", "Name", product.CategoryId);

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await products.GetByIdAsync(
                id,
                 new QueryOptions<Product>
                 {
                     Includes = new string[] { "Brand", "Category" }
                 });

            if (product == null)
                return NotFound();

            ViewBag.Brands = new SelectList(
                await brands.GetAllAsync(), "Id", "Name");

            ViewBag.Categories = new SelectList(
                await categories.GetAllAsync(), "Id", "Name");

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(
                    await brands.GetAllAsync(), "Id", "Name");

                ViewBag.Categories = new SelectList(
                    await categories.GetAllAsync(), "Id", "Name");

                return View(product);
            }

            await products.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }



    }
}

