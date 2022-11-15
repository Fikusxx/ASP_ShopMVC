using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        public void Update(ApplicationType appType);
    }
}
