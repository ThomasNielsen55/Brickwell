using Microsoft.AspNetCore.Mvc;
using Brickwell.Data;
using Brickwell.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace Brickwell.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private IBrickRepository _brickRepository;
        public AdminController(IBrickRepository brick , ILogger<AdminController> logger)
        {
            _brickRepository = brick;
            _logger = logger;   
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            Order neworder = new Order();
            return View(neworder);
        }

        [HttpPost]
        public IActionResult AddOrder(Order neworder)
        {
            if (ModelState.IsValid)
            {
                _brickRepository.AddOrder(neworder);

                var ordoos = _brickRepository.Orders.ToList();
                return View("Orders", ordoos);
            }
            else
            {
                return View(neworder);
            }
        }
        public IActionResult EditOrder(int orderid)
        {
            var orderdude = new Order();
            orderdude = _brickRepository.Orders.ToList()
                .Where(x => x.TransactionId == orderid)
                .FirstOrDefault();
            return View("AddOrder",orderdude);
        }
        public IActionResult DeleteOrderConfirmation(int orderid)
        {
            var orderdude = new Order();
            orderdude = _brickRepository.Orders.ToList()
                .Where(x => x.TransactionId == orderid)
                .FirstOrDefault();
            return View(orderdude);
        }
        public IActionResult DeleteOrder(int orderid)
        {
            var record = _brickRepository.Orders
                .Single(x => x.TransactionId == orderid);

            _brickRepository.RemoveOrder(record);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            Product newproduct = new Product();
            return View(newproduct);
        }

        [HttpPost]
        public IActionResult AddProduct(Product newproduct)
        {
            if (ModelState.IsValid)
            {
                _brickRepository.AddProduct(newproduct);

                var prodies = _brickRepository.Products.ToList();
                return View("Products", prodies);
            }
            else
            {
                return View(newproduct);
            }
        }
        public IActionResult EditProduct(int productid)
        {
            var productdude = new Product();
            productdude = _brickRepository.Products.ToList()
                .Where(x => x.ProductId == productid)
                .FirstOrDefault();
            return View("AddProduct", productdude);
        }
        public IActionResult DeleteProductConfirmation(int productid)
        {
            var productdude = new Product();
            productdude = _brickRepository.Products.ToList()
                .Where(x => x.ProductId == productid)
                .FirstOrDefault();
            return View(productdude);
        }
        public IActionResult DeleteProduct(int productid)
        {
            var record = _brickRepository.Products
                .Single(x => x.ProductId == productid);

            _brickRepository.RemoveProduct(record);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            Customer newcustomer = new Customer();
            return View(newcustomer);
        }

        [HttpPost]
        public IActionResult AddUser(Customer newcustomer)
        {
            if (ModelState.IsValid)
            {
                _brickRepository.AddUser(newcustomer);

                var custos = _brickRepository.Customers.ToList();
                return View("Customers", custos);
            }
            else
            {
                return View(newcustomer);
            }
        }
        public IActionResult EditUser(int userid)
        {
            var userdude = new Customer();
            userdude = _brickRepository.Customers.ToList()
                .Where(x => x.customer_ID == userid)
                .FirstOrDefault();
            return View("AddUser", userdude);
        }
        public IActionResult DeleteUserConfirmation(int userid)
        {
            var userdude = new Customer();
            userdude = _brickRepository.Customers.ToList()
                .Where(x => x.customer_ID == userid)
                .FirstOrDefault();
            return View(userdude);
        }

        public IActionResult DeleteUser(int userid)
        {
            var record = _brickRepository.Customers
                .Single(x => x.customer_ID == userid);

            _brickRepository.RemoveUser(record);

            return RedirectToAction("Index");
        }
        public IActionResult Orders()
        {
            var ordoos = _brickRepository.Orders.ToList();
            return View(ordoos);
        }
        public IActionResult Products()
        {
            var prodies = _brickRepository.Products.ToList();
            return View(prodies);
        }
        public IActionResult Users()
        {
            var custos = _brickRepository.Customers.ToList();
            return View(custos);
        }
    }
}
