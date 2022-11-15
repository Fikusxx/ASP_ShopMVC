using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void Update(OrderHeader orderHeader);
    }
}
