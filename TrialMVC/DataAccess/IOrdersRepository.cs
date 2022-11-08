using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.Data
{
    public interface IOrderRepository
    {
        public Order? GetOrderById(int id);
        public OrderItem? GetItemOrderById(int id);
        public IEnumerable<Order?> GetAllOrders();
        public IEnumerable<Provider?> GetAllProviders();
        public IEnumerable<OrderItem?> GetAllOrderItems();
        public Order? AddOrder(Order order);
        public OrderItem? AddOrderItem(OrderItem orderItem);
        public Order? UpdateOrder(Order order);
        public OrderItem? UpdateOrderItem(OrderItem item);
        public Order? DeleteOrderById(int id);
        public OrderItem? DeleteItemById(int id);
    }
}
