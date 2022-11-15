using ShopMVC_Models;

namespace ShopMVC_ViewModels
{
	public class ProductUserViewModel
	{
		public ApplicationUser ApplicationUser { get; set; }
		public List<Product> Products { get; set; } = new();
	}
}
