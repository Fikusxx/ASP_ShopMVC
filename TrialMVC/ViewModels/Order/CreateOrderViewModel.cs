using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TrialMVC.ViewModels
{
	public class CreateOrderViewModel
	{
		[Required]
		public string OrderNumber { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime? OrderDate { get; set; }

		[Required(ErrorMessage = "Invalid order provider")]
		public int ProviderId { get; set; }

		public SelectList? ProvidersSelectList { get; set; }

	}
}
