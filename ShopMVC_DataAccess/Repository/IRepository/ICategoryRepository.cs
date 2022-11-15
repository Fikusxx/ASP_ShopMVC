using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public void Update(Category category);
    }
}
