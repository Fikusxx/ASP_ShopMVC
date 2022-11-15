using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationDbContext database) : base(database)
        { }

        public void Update(OrderDetails obj)
        {
            db.OrderDetails.Update(obj);
        }
    }
}
