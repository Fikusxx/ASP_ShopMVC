using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product product);

        /// <summary>
        /// string obj defines which DbSet<T> to use.
        /// DbSet<T> converted into new SelectList (DbSet>T>, "Id", "Name")
        /// </summary>
        public SelectList GetSelectList(string obj);
    }
}
