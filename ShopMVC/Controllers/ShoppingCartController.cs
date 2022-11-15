using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;
using ShopMVC_ViewModels;
using System.Security.Claims;
using System.Text;

namespace ShopMVC.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository productDB;
        private readonly IInquiryHeaderRepository inquiryHeaderDB;
        private readonly IInquiryDetailsRepository inquiryDetailsDB;
        private readonly IOrderHeaderRepository orderHeaderDB;
        private readonly IOrderDetailsRepository orderDetailsDB;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBrainTreeGate brain;

        [BindProperty]
        public ProductUserViewModel ProductUserViewModel { get; set; } = null!;

        public ShoppingCartController(IProductRepository productDB,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            IInquiryHeaderRepository inquiryHeaderDB,
            IInquiryDetailsRepository inquiryDetailsDB,
            IOrderHeaderRepository orderHeaderDB,
            IOrderDetailsRepository orderDetailsDB,
            IBrainTreeGate brain)
        {
            this.productDB = productDB;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.inquiryHeaderDB = inquiryHeaderDB;
            this.inquiryDetailsDB = inquiryDetailsDB;
            this.orderHeaderDB = orderHeaderDB;
            this.orderDetailsDB = orderDetailsDB;
            this.brain = brain; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            if (shoppingCartList == null)
            {
                shoppingCartList = new List<ShoppingCart>();
            }

            List<int> productsIdInCart = shoppingCartList.Select(x => x.ProductId).ToList();
            List<Product> productsList = productDB.GetAll(predicate: x => productsIdInCart.Contains(x.Id)).ToList();

            foreach (var cartItem in shoppingCartList)
            {
                Product? prodTemp = productsList.FirstOrDefault(x => x.Id == cartItem.ProductId);

                if(prodTemp != null)
                    prodTemp.TempSqft = cartItem.Sqft;
            }

            return View(productsList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Product> productList)
        {
            var shoppingCartList = new List<ShoppingCart>();

            foreach (var product in productList)
            {
                shoppingCartList.Add(new ShoppingCart() { ProductId = product.Id, Sqft = product.TempSqft });
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Summary));
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            ApplicationUser applicationUser = null;

            if (User.IsInRole(WebConstants.AdminRole))
            {
                var sessionInquiryId = HttpContext.Session.Get<int>(WebConstants.SessionInquiryId);

                if (sessionInquiryId != 0)
                {
                    var inquiryHeader = inquiryHeaderDB.FindById(sessionInquiryId);

                    applicationUser = new ApplicationUser()
                    {
                        FullName = inquiryHeader.FullName,
                        Email = inquiryHeader.Email,
                        PhoneNumber = inquiryHeader.PhoneNumber
                    };
                }
                else
                {
                    applicationUser = new ApplicationUser();
                }

                var gateway = brain.GetGateway();
                var clientToken = gateway.ClientToken.Generate();
                ViewBag.ClientToken = clientToken;
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                applicationUser = await userManager.FindByIdAsync(userId);
            }

            var shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            if (shoppingCartList == null && shoppingCartList.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            List<int> productsIdInCart = shoppingCartList.Select(x => x.ProductId).ToList();
            List<Product> productsList = productDB.GetAll(predicate: x => productsIdInCart.Contains(x.Id)).ToList();

            ProductUserViewModel = new ProductUserViewModel()
            {
                ApplicationUser = applicationUser,
            };

            foreach (var cartItem in shoppingCartList)
            {
                Product? tempProduct = productDB.FindById(cartItem.ProductId);

                if (tempProduct != null)
                {
                    tempProduct.TempSqft = cartItem.Sqft;
                    ProductUserViewModel.Products.Add(tempProduct);
                }  
            }

            return View(ProductUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(ProductUserViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pathToTemplate = webHostEnvironment.WebRootPath + "/templates/Inquiry.html";

            if (User.IsInRole(WebConstants.AdminRole))
            {
                var orderTotal = model.Products.Sum(x => x.Price * x.TempSqft);

                var orderHeader = new OrderHeader()
                {
                    CreatedByUserId = userId,
                    FinalOrderTotal = orderTotal,
                    City = model.ApplicationUser.City,
                    StreetAddress = model.ApplicationUser.StreetAdress,
                    State = model.ApplicationUser.State,
                    PostalCode = model.ApplicationUser.PostalCode,
                    FullName = model.ApplicationUser.FullName,
                    Email = model.ApplicationUser.Email,
                    PhoneNumber = model.ApplicationUser.PhoneNumber,
                    OrderDate = DateTime.Now,
                    OrderStatus = OrderStatus.Pending.ToString()
                };

                orderHeaderDB.Add(orderHeader);
                orderHeaderDB.Save();

                foreach (var product in model.Products)
                {
                    var orderDetails = new OrderDetails()
                    {
                        OrderHeaderId = orderHeader.Id,
                        PricePerSqFt = product.Price,
                        Sqft = product.TempSqft,
                        ProductId = product.Id
                    };

                    orderDetailsDB.Add(orderDetails);
                }

                orderDetailsDB.Save();

                return RedirectToAction(nameof(InquiryConfirmation), new {Id = orderHeader.Id});
            }
            else
            {
                var subject = "New Inquiry";
                var htmlBody = "";

                using (var sr = System.IO.File.OpenText(pathToTemplate))
                {
                    htmlBody = sr.ReadToEnd();
                }

                var sb = new StringBuilder();

                foreach (var prod in model.Products)
                {
                    sb.Append($"  - Name: {prod.Name} <span style='font-size:14px;'> (ID: {prod.Id})</span><br/>");
                }

                string messageBody = string.Format(htmlBody,
                    model.ApplicationUser.FullName,
                    model.ApplicationUser.Email,
                    model.ApplicationUser.PhoneNumber,
                    sb.ToString());

                // Send email using strings above

                var inquiryHeader = new InquiryHeader()
                {
                    ApplicationUserId = userId,
                    FullName = model.ApplicationUser.FullName ?? "",
                    PhoneNumber = model.ApplicationUser.PhoneNumber,
                    Email = model.ApplicationUser.Email,
                    InquiryDate = DateTime.Now
                };

                inquiryHeaderDB.Add(inquiryHeader);
                inquiryHeaderDB.Save();

                foreach (var product in model.Products)
                {
                    var inquiryDetails = new InquiryDetails()
                    {
                        InquiryHeaderId = inquiryHeader.Id,
                        ProductId = product.Id
                    };

                    inquiryDetailsDB.Add(inquiryDetails);
                }

                inquiryDetailsDB.Save();
            }

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        [HttpGet]
        public IActionResult InquiryConfirmation(int? id)
        {
            var orderHeader = orderHeaderDB.FindById(id.GetValueOrDefault());

            HttpContext.Session.Clear();
            return View(orderHeader);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var product = productDB.FirstOrDefault(predicate: x => x.Id == id);

            if (product == null)
                return NotFound();

            var shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            if (shoppingCartList != null && shoppingCartList.Count > 0)
            {
                var shoppingCartItem = shoppingCartList.FirstOrDefault(x => x.ProductId == product.Id);

                if (shoppingCartItem != null)
                {
                    shoppingCartList.Remove(shoppingCartItem);
                    HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(List<Product> productList)
        {
            var shoppingCartList = new List<ShoppingCart>();

            foreach (var product in productList)
            {
                shoppingCartList.Add(new ShoppingCart() { ProductId = product.Id, Sqft = product.TempSqft });
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
