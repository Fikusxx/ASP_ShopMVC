using ShopMVC_Models;

namespace ShopMVC_ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }

	}
}
