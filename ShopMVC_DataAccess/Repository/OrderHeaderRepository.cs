using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepository(ApplicationDbContext database) : base(database)
        { }

        public void Update(OrderHeader obj)
        {
            db.OrderHeaders.Update(obj);
        }
    }
}
