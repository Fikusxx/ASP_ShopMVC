using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(ApplicationDbContext database) : base(database)
		{ }

		public void Update(Category obj)
		{
			var category = db.Categories.FirstOrDefault(x => x.Id == obj.Id);

			if (category != null)
			{
				category.Name = obj.Name;
				category.DisplayOrder = obj.DisplayOrder;
				db.Categories.Update(category);
			}
		}
	}
}
