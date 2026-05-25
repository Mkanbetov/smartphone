using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartphone.Models;
using Smartphone.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smartphone.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _repo;

        public CustomerController(IRepository<Customer> repo)
        {
            _repo = repo;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Customer customer)
        {

            await _repo.AddAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            

            await _repo.UpdateAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _repo.GetByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


