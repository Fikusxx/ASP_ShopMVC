using Microsoft.AspNetCore.Mvc;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;
using ShopMVC_ViewModels;
using System.Diagnostics;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductRepository productDB;
        private readonly ICategoryRepository categoryDB;

        public HomeController(ILogger<HomeController> logger, IProductRepository productDB, ICategoryRepository categoryDB)
        {
            this.logger = logger;
            this.productDB = productDB;
            this.categoryDB = categoryDB;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var includeProperties = new List<string>()
            {
                WebConstants.CategoryName,
                WebConstants.ApplicationTypeName
            };

            var products = productDB.GetAll(includeProperties: includeProperties);
            var categories = categoryDB.GetAll().ToList();

            var model = new HomeViewModel() 
            {
                Products = products,
                Categories = categories
            }; 

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var includeProperties = new List<string>()
            {
                WebConstants.CategoryName,
                WebConstants.ApplicationTypeName
            };

            var product = productDB.FirstOrDefault(predicate: x => x.Id == id, includeProperties: includeProperties);

            if (product == null)
                return NotFound();

            var sessionShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            bool isProductInCart = false;

            if (sessionShoppingCartList != null)
            {
                isProductInCart = sessionShoppingCartList.Any(x => x.ProductId == id);
            }

            var model = new HomeDetailsViewModel()
            {
                Product = product,
                ExistsInCart = isProductInCart
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int id, HomeDetailsViewModel model)
        {
            var sessionShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            if (sessionShoppingCartList == null)
            {
                sessionShoppingCartList = new List<ShoppingCart>();
            }

            sessionShoppingCartList.Add(new ShoppingCart() { ProductId = id, Sqft = model.Product.TempSqft});
            HttpContext.Session.Set(WebConstants.SessionCart, sessionShoppingCartList);

            TempData[WebConstants.Success] = "Item added to cart successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int id)
        {
            var product = productDB.FindById(id);

            if (product == null)
                return NotFound();

            var sessionShoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            if (sessionShoppingCartList != null)
            {
                var productToRemove = sessionShoppingCartList.FirstOrDefault(x => x.ProductId == product.Id);

                if (productToRemove != null)
                {
                    sessionShoppingCartList.Remove(productToRemove);
                    HttpContext.Session.Set(WebConstants.SessionCart, sessionShoppingCartList);
                }
            }

            TempData[WebConstants.Error] = "Item was removed from cart!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}