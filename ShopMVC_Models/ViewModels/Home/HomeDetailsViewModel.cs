using ShopMVC_Models;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_ViewModels
{
	public class HomeDetailsViewModel
	{
		public Product Product { get; set; } = new Product();

		public bool ExistsInCart { get; set; }

	}
}
