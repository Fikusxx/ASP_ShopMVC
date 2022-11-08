using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrialMVC.Data;
using TrialMVC.DataAccess.Data_Models;
using TrialMVC.Utilities;
using TrialMVC.ViewModels;

namespace TrialMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository db;
        public OrderController(IOrderRepository db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = GetDefaultIndexViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FilteredOrdersViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.From.HasValue == false)
                    model.From = DateTime.Now - TimeSpan.FromDays(30);

                if (model.To.HasValue == false)
                    model.To = DateTime.Now;

                IQueryable<Order> orders = db.GetAllOrders().AsQueryable()!;

                orders = orders.Where(x => x.Date >= model.From.Value && x.Date <= model.To.Value);

                if (model.OrderNumberList.Any())
                    orders = orders.Where(x => model.OrderNumberList.Contains(x.Number));

                if (model.ProviderIdList.Any())
                    orders = orders.Where(x => model.ProviderIdList.Contains(x.ProviderId));

                if (model.ProviderNameList.Any())
                    orders = orders.Where(x => model.ProviderNameList.Contains(x.Provider.Name));

                var ordersList = orders.ToList();
                model = GetDefaultIndexViewModel(ordersList);

                return View(model);
            }

            return View(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var providers = db.GetAllProviders().ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");
            var model = new CreateOrderViewModel()
            {
                ProvidersSelectList = providersSelectList
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    Number = model.OrderNumber,
                    Date = model.OrderDate!.Value,
                    ProviderId = model.ProviderId,
                };

                db.AddOrder(order);

                return RedirectToAction(nameof(Index));
            }

            var providers = db.GetAllProviders().ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");
            model.ProvidersSelectList = providersSelectList;

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = db.GetOrderById(id);

            if (order == null)
                return NotFound();

            var model = new FilteredOrderItemsInOrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
                OrderItems = order.OrderItems,
            };

            model = GetDefaultDetailsViewModel(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(FilteredOrderItemsInOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                IQueryable<OrderItem> orderItems = db.GetAllOrderItems().AsQueryable()!;
                orderItems = orderItems.Where(x => x.OrderId == model.Id);

                if (model.OrderItemsNameList.Any())
                    orderItems = orderItems.Where(x => model.OrderItemsNameList.Contains(x.Name));

                if (model.OrderItemsUnitList.Any())
                    orderItems = orderItems.Where(x => model.OrderItemsUnitList.Contains(x.Unit));

                var orderItemsList = orderItems.ToList();
                model.OrderItems = orderItemsList;
                model = GetDefaultDetailsViewModel(model);

                return View(model);
            }

            return View(nameof(Details), new { Id = model.Id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = db.GetOrderById(id);

            if (order == null)
                return NotFound();

            var model = new EditOrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
            };

            var providers = db.GetAllProviders().ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");
            model.ProvidersSelectList = providersSelectList;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    Id = model.Id,
                    Number = model.Number,
                    Date = model.Date,
                    ProviderId = model.ProviderId
                };

                db.UpdateOrder(order);

                return RedirectToAction(nameof(Details), new { Id = model.Id });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var order = db.GetOrderById(id);

            if (order == null)
                return NotFound();

            var model = new DeleteOrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
            };

            var providers = db.GetAllProviders().ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");
            model.ProvidersSelectList = providersSelectList;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.DeleteOrderById(model.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private FilteredOrdersViewModel GetDefaultIndexViewModel(List<Order> orders = null)
        {
            SelectList orderNumberSelectList = null;

            if (orders == null)
            {
                orders = db.GetAllOrders().ToList();
                orderNumberSelectList = new SelectList(orders.Select(x => x.Number).Distinct());
            }
            else
            {
                var newOrders = db.GetAllOrders().ToList();
                orderNumberSelectList = new SelectList(newOrders.Select(x => x.Number).Distinct());
            }

            var providers = db.GetAllProviders().ToList();
            var prodiversIdSelectList = new SelectList(providers.Select(x => x.Id).Distinct());
            var providersNameSelectList = new SelectList(providers.Select(x => x.Name).Distinct());

            var model = new FilteredOrdersViewModel()
            {
                OrderNumberSelectList = orderNumberSelectList,
                ProviderIdSelectList = prodiversIdSelectList,
                ProviderNameSelectList = providersNameSelectList,
                OrderList = orders,
            };

            return model;
        }

        [NonAction]
        private FilteredOrderItemsInOrderViewModel GetDefaultDetailsViewModel(FilteredOrderItemsInOrderViewModel model)
        {
            var providers = db.GetAllProviders().ToList();
            var providersSelectList = new SelectList(providers, "Id", "Name");

            var orderItems = db.GetAllOrderItems().ToList();
            var orderItemsNameSelectList = new SelectList(orderItems.Select(x => x.Name).Distinct());
            var orderItemsUnutSelectList = new SelectList(orderItems.Select(x => x.Unit).Distinct());

            model.ProvidersSelectList = providersSelectList;
            model.OrderItemsNameSelectList = orderItemsNameSelectList;
            model.OrderItemsUnitSelectList = orderItemsUnutSelectList;

            return model;
        }
    }
}
