using Brickwell.Data;
using Brickwell.Data.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.Identity.Client;
using System.Drawing.Printing;

namespace BrickedUpBrickBuyer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrickRepository _brickRepository;
        private readonly InferenceSession _session;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IBrickRepository brick, ILogger<HomeController> logger)
        {
            _brickRepository = brick;
            _logger = logger;

            // Initialize inference session. Make sure path is correct
            try
            {
                _session = new InferenceSession("hist_grad_boost.onnx");
                _logger.LogInformation("Onnx model loaded successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading the ONNX model: {ex.Message}");
            }
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
		public IActionResult GetCheckout()
		{
			return View();
		}
		[HttpGet]
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Products(string primaryColor, string category, int pageSize, int pageNum = 1)
        {
            if (pageSize == 0)
            {
                pageSize = 5;
            }

            var productInfos = new ProductsPagesViewModel
            {
                Products = _brickRepository.Products
                            .Where(x => (primaryColor == null || x.PrimaryColor == primaryColor || x.SecondaryColor == primaryColor) && (x.Category.Contains(category) || category == null))
                            .OrderByDescending(x => x.Price)
                            .Skip((pageNum - 1) * pageSize)
                            .Take(pageSize),
                CurrentColor = primaryColor,
                CurrentCategory = category,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _brickRepository.Products
                                .Where(x => (primaryColor == null || x.PrimaryColor == primaryColor || x.SecondaryColor == primaryColor) && (x.Category.Contains(category) || category == null))
                                .OrderBy(x => x.Name)
                                .Count()
                },
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

        public IActionResult Test(int ordernum = 737060)
        {
            var orderguy = new Order();
            orderguy = _brickRepository.Orders.ToList()
                .Where(x => x.TransactionId == ordernum)
                .FirstOrDefault();

            return View(orderguy);
        }

        public IActionResult Orders(int ordernum) 
        {
            var order = new Order();
            order = _brickRepository.Orders.ToList()
                .Where(x => x.TransactionId == ordernum)
                .FirstOrDefault();
            //var records = (from order in _brickRepository.Orders
            //               join customer in _brickRepository.Customers
            //               on order.CustomerId equals customer.customer_ID
            //               orderby order.Date descending
            //               select new
            //               {
            //                   Customer = customer,
            //                   Order = order
            //               }).Take(20);

            //   var predictions = new List<OrderPrediction>(); // Need to figure out what this will be...

            var record = new RecordViewModel()
            {
                Order = order,
                Customer = _brickRepository.Customers
                .Where(x => x.customer_ID == order.CustomerId)
                .FirstOrDefault()
            };


            var class_type_dict = new Dictionary<int, string>
                {
                    {0, "Not Fraud" },
                    {1, "Fraud" }
                };

            //foreach (var record in records)
            //{
            // Make the data compatible with the models
            var input = new List<float>
                {
                    (float)record.Customer.age,
                    (float)record.Order.Time,
                    (float)(record.Order.Amount),

                    // Check the dummy data
                    record.Customer.country_of_residence == "India" ? 1 : 0,
                    record.Customer.country_of_residence == "Russia" ? 1 : 0,
                    record.Customer.country_of_residence == "USA" ? 1 : 0,
                    record.Customer.country_of_residence == "UnitedKingdom" ? 1 : 0,

                    record.Customer.gender == "M" ? 1 : 0,

                    record.Order.DayOfWeek == "Mon" ? 1 : 0,
                    record.Order.DayOfWeek == "Sat" ? 1 : 0,
                    record.Order.DayOfWeek == "Sun" ? 1 : 0,
                    record.Order.DayOfWeek == "Thu" ? 1 : 0,
                    record.Order.DayOfWeek == "Tue" ? 1 : 0,
                    record.Order.DayOfWeek == "Wed" ? 1 : 0,

                    record.Order.EntryMode == "PIN" ? 1 : 0,
                    record.Order.EntryMode == "Tap" ? 1 : 0,

                    record.Order.TypeOfTransaction == "Online" ? 1 : 0,
                    record.Order.TypeOfTransaction == "POS" ? 1 : 0,

                    record.Order.CountryOfTransaction == "India" ? 1 : 0,
                    record.Order.CountryOfTransaction == "Russia" ? 1 : 0,
                    record.Order.CountryOfTransaction == "USA" ? 1 : 0,
                    record.Order.CountryOfTransaction == "UnitedKingdom" ? 1 : 0,

                    record.Order.ShippingAddress == "India" ? 1 : 0,
                    record.Order.ShippingAddress == "Russia" ? 1 : 0,
                    record.Order.ShippingAddress == "USA" ? 1 : 0,
                    record.Order.ShippingAddress == "UnitedKingdom" ? 1 : 0,
                    
                    record.Order.Bank == "HSBC" ? 1 : 0,
                    record.Order.Bank == "Halifax" ? 1 : 0,
                    record.Order.Bank == "Lloyds" ? 1 : 0,
                    record.Order.Bank == "Metro" ? 1 : 0,
                    record.Order.Bank == "Monzo" ? 1 : 0,
                    record.Order.Bank == "RBS" ? 1 : 0,

                    record.Order.TypeOfCard == "Visa" ? 1 : 0
                };

            var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
            };

            var Fraud = new int();

            using (var results = _session.Run(inputs))
            {
                var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                if (prediction != null && prediction.Length > 0)
                {
                    // Use the prediction to get the animal type from the dictionary
                    Fraud = (int)prediction[0];
                }
                else
                {
                    ViewBag.Prediction = "Error: Unable to make a prediction.";
                }

            }

            if (Fraud == 0)
            {
                //_brickRepository.AddOrder(order);
                return View("Confirmation");
            }
            else
            {
                //_brickRepository.AddOrder(order);
                return View("Review");
            }
            // predictions.Add(new OrderPrediction {Orders = record.Order, Customer = record.Customer});
            //}
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

