using Brickwell.Data;
using Brickwell.Data.ViewModels;




using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BrickedUpBrickBuyer.Controllers
{
    public class HomeController : Controller
    {
        private IBrickRepository _brickRepository;
        public HomeController(IBrickRepository brick)
        {
            _brickRepository = brick;
        }

        public IActionResult Index()
        {
            var Bricks = _brickRepository.Customers.ToList();


            return View(Bricks);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult AccountCreate()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Products(string primaryColor, string category)
        {
            //var products = _brickRepository.Products;
            //var ViewModel= new FilterViewModel

            //{
            //    Categories = _brickRepository.Products
            // .Select(x => x.Category ?? "")
            // .Where(x => !string.IsNullOrWhiteSpace(x))
            // .Distinct()
            // .OrderBy(x => x)
            //};

            //var modifiedCategories = ViewModel.Categories.Select(m => m.Split(" - ").FirstOrDefault()).ToList();

            //ViewModel.Categories = modifiedCategories;

            var productInfos = new ProductsPagesViewModel
            {
                Products = _brickRepository.Products
                .Where(x => (primaryColor == null || x.PrimaryColor == primaryColor || x.SecondaryColor == primaryColor) && (x.Category.Contains(category) || category == null))
                .OrderBy(x => x.Name),
                CurrentColor = primaryColor,
                CurrentCategory = category,
            };
            return View(productInfos);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SelectedProduct(int productnum = 1)
        {
            var Productguy = new Product();
            Productguy = _brickRepository.Products.ToList()
                .Where(x => x.ProductId == productnum)
                .FirstOrDefault();

            return View(Productguy);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

