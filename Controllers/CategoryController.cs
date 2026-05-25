using Microsoft.AspNetCore.Mvc;
using Smartphone.Models;
using Smartphone.Data;
using System.Threading.Tasks;

namespace Smartphone.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repo;

        public CategoryController(IRepository<Category> repo)
        {
            _repo = repo;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
         

            await _repo.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
           

            await _repo.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


