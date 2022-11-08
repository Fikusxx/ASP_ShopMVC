using Microsoft.AspNetCore.Mvc;
using TrialMVC.Data;
using TrialMVC.DataAccess.Data_Models;
using TrialMVC.ViewModels;

namespace TrialMVC.Controllers
{
	public class OrderItemController : Controller
	{
        private readonly IOrderRepository db;
        public OrderItemController(IOrderRepository db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var order = db.GetOrderById(id);

            if (order == null)
                return NotFound();

            var model = new NewOrderItemViewModel()
            {
                OrderId = id,
                OrderNumber = order.Number
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewOrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderItem = new OrderItem()
                {
                    Name = model.ItemName,
                    Quantity = model.Quantity,
                    Unit = model.Unit,
                    OrderId = model.OrderId,
                };

                db.AddOrderItem(orderItem);

                return RedirectToAction("Details", "Order", new { Id = model.OrderId });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderItem = db.GetItemOrderById(id);

            if (orderItem == null)
                return NotFound();

            var model = new EditOrderItemViewModel()
            {
                OrderItemId = orderItem.Id,
                OrderId = orderItem.Order.Id,
                OrderNumber = orderItem.Order.Number,
                ItemName = orderItem.Name,
                Quantity = orderItem.Quantity,
                Unit = orderItem.Unit
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditOrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderItem = new OrderItem()
                {
                    Id = model.OrderItemId,
                    Name = model.ItemName,
                    Quantity = model.Quantity,
                    Unit = model.Unit,
                    OrderId = model.OrderId
                };

                db.UpdateOrderItem(orderItem);

                return RedirectToAction("Details", "Order", new { Id = model.OrderId });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var orderItem = db.GetItemOrderById(id);

            if (orderItem == null)
                return NotFound();

            var model = new DeleteOrderItemViewModel()
            {
                OrderId = orderItem.OrderId,
                OrderNumber = orderItem.Order.Number,
                OrderItemId = orderItem.Id,
                ItemName = orderItem.Name,
                Quantity = orderItem.Quantity,
                Unit = orderItem.Unit
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteOrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.DeleteItemById(model.OrderItemId);

                return RedirectToAction("Details", "Order", new { Id = model.OrderId });
            }

            return View(model);
        }
    }
}
