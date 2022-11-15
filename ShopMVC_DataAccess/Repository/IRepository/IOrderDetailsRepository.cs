using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        public void Update(OrderDetails orderDetails);
    }
}
