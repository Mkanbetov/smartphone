using Microsoft.AspNetCore.Mvc;
using Smartphone.Data;
using Smartphone.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smartphone.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orders;
        private readonly IRepository<Customer> _customers;
        private readonly IRepository<Product> _products;

        public OrderController(IRepository<Order> orders,
                               IRepository<Customer> customers,
                               IRepository<Product> products)
        {
            _orders = orders;
            _customers = customers;
            _products = products;
        }

        // GET: /Order/Create?productId=1
        [HttpGet]
        public async Task<IActionResult> Create(int productId)
        {
            var product = await _products.GetByIdAsync(productId);
            if (product == null) return NotFound();

            // Список покупателей для dropdown
            var customers = await _customers.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "Id", "FullName");
            ViewBag.Product = product;

            return View();
        }

        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, int customerId)
        {
            var product = await _products.GetByIdAsync(productId);
            var customer = await _customers.GetByIdAsync(customerId);

            if (product == null || customer == null)
                return NotFound();

            var order = new Order
            {
                ProductId = productId,
                CustomerId = customerId,
                CustomerName = customer.FullName
            };

            await _orders.AddAsync(order);

            

            return RedirectToAction("Success");
        }

        // GET: /Order/Index
        public async Task<IActionResult> Index()
        {
            var orders = await _orders.GetAllAsync();
            return View(orders);
        }
        // GET: /Order/Success
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

    }
}

