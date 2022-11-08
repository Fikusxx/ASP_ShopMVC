using Microsoft.EntityFrameworkCore;
using TrialMVC.DataAccess;
using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.Data
{
    public class SQLOrdersRepository : IOrderRepository
    {
        private readonly ApplicationDbContext db;
        public SQLOrdersRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Order? GetOrderById(int id)
        {
            return db.Orders.Include(x => x.Provider).Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders.Include(x => x.Provider).Include(x => x.OrderItems).ToList();
        }

        public Order? AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return order;
        }

        public Order? DeleteOrderById(int id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            db.Orders.Remove(order);
            db.SaveChanges();

            return order;
        }

        public Order? UpdateOrder(Order orderChanges)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == orderChanges.Id);

            if (order != null)
            {
                order.Number = orderChanges.Number;
                order.ProviderId = orderChanges.ProviderId;
                order.Date = orderChanges.Date;

                db.Orders.Update(order);
                db.SaveChanges();
            }

            return order;
        }

        public OrderItem? UpdateOrderItem(OrderItem itemChanges)
        {
            var item = db.OrderItems.FirstOrDefault(x => x.Id == itemChanges.Id);

            if (item != null)
            {
                item.Name = itemChanges.Name;
                item.Quantity = itemChanges.Quantity;
                item.Unit = itemChanges.Unit;

                db.OrderItems.Update(item);
                db.SaveChanges();
            }

            return item;
        }

        public OrderItem? AddOrderItem(OrderItem orderItem)
        {
            db.OrderItems.Add(orderItem);
            db.SaveChanges();

            return orderItem;
        }

        public OrderItem? DeleteItemById(int id)
        {
            var orderItem = db.OrderItems.FirstOrDefault(x => x.Id == id);
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();

            return orderItem;
        }

        public IEnumerable<Provider?> GetAllProviders()
        {
            return db.Providers;
        }

        public OrderItem? GetItemOrderById(int id)
        {
            return db.OrderItems.Include(x => x.Order).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<OrderItem?> GetAllOrderItems()
        {
            return db.OrderItems;
        }
    }
}
