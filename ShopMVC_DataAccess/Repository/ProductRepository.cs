using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;
using ShopMVC_Utility;

namespace ShopMVC_DataAccess
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext database) : base(database)
        { }

        public void Update(Product obj)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == obj.Id);

            if (product != null)
            {
                product.Name = obj.Name;
                product.Description = obj.Description;
                product.ShortDescription = obj.ShortDescription;
                product.Price = obj.Price;
                product.PhotoPath = obj.PhotoPath;
                product.CategoryId = obj.CategoryId;
                product.ApplicationTypeId = obj.ApplicationTypeId;

                db.Products.Update(product);
            }
        }

        public SelectList GetSelectList(string obj)
        {
            switch (obj)
            {
                case WebConstants.CategoryName:
                    return new SelectList(db.Categories, "Id", "Name");

                case WebConstants.ApplicationTypeName:
                    return new SelectList(db.ApplicationTypes, "Id", "Name");

                default:
                    return null;
            }
        }
    }
}
