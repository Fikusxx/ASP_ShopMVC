using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class OrderController : Controller
    {
        private readonly IOrderHeaderRepository orderHeaderDB;
        private readonly IOrderDetailsRepository orderDetailsDB;
        private readonly IBrainTreeGate brain;

        public OrderController(IOrderHeaderRepository orderHeaderDB,
            IOrderDetailsRepository orderDetailsDB,
            IBrainTreeGate brain)
        {
            this.orderHeaderDB = orderHeaderDB;
            this.orderDetailsDB = orderDetailsDB;
            this.brain = brain;
        }

        [HttpGet]
        public IActionResult Index(string searchName = null, 
            string searchEmail = null,
            string searchPhone = null,
            string Status = null)
        {
            var statusSelectList = Enum.GetNames<OrderStatus>().ToList();

            var model = new OrderListViewModel()
            {
                OrderHeadersList = orderHeaderDB.GetAll().ToList(),
                StatusList = new SelectList(statusSelectList),

            };

            if (searchName != null)
                model.OrderHeadersList = model.OrderHeadersList.Where(x => x.FullName.ToLower()
                                                               .Contains(searchName.ToLower())).ToList();

            if(searchEmail != null)
                model.OrderHeadersList = model.OrderHeadersList.Where(x => x.Email.ToLower()
                                                               .Contains(searchEmail.ToLower())).ToList();

            if(searchPhone != null)
                model.OrderHeadersList = model.OrderHeadersList.Where(x => x.PhoneNumber.ToLower()
                                                               .Contains(searchPhone.ToLower())).ToList();

            if (Status != null)
                model.OrderHeadersList = model.OrderHeadersList.Where(x => x.OrderStatus == Status).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var orderHeader = orderHeaderDB.FindById(id.GetValueOrDefault());

            if (orderHeader == null)
                return NotFound();

            var includeProperties = new List<string>() { WebConstants.ProductName };
            var orderDetailsList = orderDetailsDB.GetAll(predicate: x => x.OrderHeaderId == orderHeader.Id,
                                                         includeProperties: includeProperties);

            var model = new OrderViewModel()
            {
                OrderHeader = orderHeader,
                OrderDetailsList = orderDetailsList.ToList()
            };

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcessing(OrderViewModel model)
        {
            var orderHeader = orderHeaderDB.FindById(model.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound();

            orderHeader.OrderStatus = OrderStatus.InProcess.ToString();
            orderHeaderDB.Update(orderHeader);
            orderHeaderDB.Save();

            TempData[WebConstants.Success] = "Order is in process";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShipOrder(OrderViewModel model)
        {
            var orderHeader = orderHeaderDB.FindById(model.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound();

            orderHeader.OrderStatus = OrderStatus.Shipped.ToString();
            orderHeader.ShippingDate = DateTime.Now;
            orderHeaderDB.Update(orderHeader);
            orderHeaderDB.Save();

            TempData[WebConstants.Success] = "Order shipped successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(OrderViewModel model)
        {
            var orderHeader = orderHeaderDB.FindById(model.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound();

            orderHeader.OrderStatus = OrderStatus.Refunded.ToString();
            orderHeaderDB.Update(orderHeader);
            orderHeaderDB.Save();

            TempData[WebConstants.Success] = "Order cancelled successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetails(OrderViewModel model)
        {
            var orderHeader = orderHeaderDB.FindById(model.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound();

            orderHeader.FullName = model.OrderHeader.FullName;
            orderHeader.PhoneNumber = model.OrderHeader.PhoneNumber;
            orderHeader.StreetAddress = model.OrderHeader.StreetAddress;
            orderHeader.City = model.OrderHeader.City;
            orderHeader.State = model.OrderHeader.State;
            orderHeader.PostalCode = model.OrderHeader.PostalCode;
            orderHeader.Email = model.OrderHeader.Email;

            orderHeaderDB.Update(orderHeader);
            orderHeaderDB.Save();

            TempData[WebConstants.Success] = "Order details updated successfully";

            return RedirectToAction(nameof(Details), new { Id = orderHeader.Id});
        }
    }
}


