using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;
using ShopMVC_ViewModels;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class InquiryController : Controller
    {
        [BindProperty]
        public InquiryViewModel InquiryViewModel { get; set; }
        private readonly IInquiryHeaderRepository inquiryHeaderDB;
        private readonly IInquiryDetailsRepository inquiryDetailsDB;

        public InquiryController(IInquiryHeaderRepository inquiryHeaderDB,
            IInquiryDetailsRepository inquiryDetailsDB)
        {
            this.inquiryHeaderDB = inquiryHeaderDB;
            this.inquiryDetailsDB = inquiryDetailsDB;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var inquiryHeader = inquiryHeaderDB.FindById(id.GetValueOrDefault());

            if (inquiryHeader == null)
                return NotFound();

            var includeProperties = new List<string>() { WebConstants.ProductName };
            var inquiryDetailsList = inquiryDetailsDB.GetAll(predicate: x => x.InquiryHeaderId == id,
                includeProperties: includeProperties).ToList();

            InquiryViewModel = new InquiryViewModel()
            {
                InquiryHeader = inquiryHeader,
                InquiryDetailsList = inquiryDetailsList
            };

            return View(InquiryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(InquiryViewModel model)
        {
            InquiryViewModel.InquiryDetailsList = inquiryDetailsDB.GetAll(predicate: x => x.InquiryHeaderId == model.InquiryHeader.Id,
                includeProperties: new List<string>() { WebConstants.ProductName }).ToList();

            var shoppingCartList = new List<ShoppingCart>();

            shoppingCartList = InquiryViewModel.InquiryDetailsList.Select(x => new ShoppingCart()
            {
                ProductId = x.ProductId,
                Sqft = x.Product.TempSqft
            }).ToList();

            HttpContext.Session.Clear();
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            HttpContext.Session.Set(WebConstants.SessionInquiryId, model.InquiryHeader.Id);

            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(InquiryViewModel model)
        {
            var inquiryHeader = inquiryHeaderDB.FindById(model.InquiryHeader.Id);

            if (inquiryHeader == null)
                return NotFound();

            var inquiryDetails = inquiryDetailsDB.GetAll(predicate: x => x.InquiryHeaderId == inquiryHeader.Id)
                                                  .ToList();

            inquiryHeaderDB.Remove(inquiryHeader);
            inquiryDetails.ForEach(x => inquiryDetailsDB.Remove(x));
            inquiryDetailsDB.Save();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = inquiryHeaderDB.GetAll() });
        }

        #endregion
    }
}
