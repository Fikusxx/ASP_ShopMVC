using System.ComponentModel.DataAnnotations;

namespace TrialMVC.ViewModels
{
	public class DeleteOrderItemViewModel
	{
        public int OrderId { get; set; }

        public string? OrderNumber { get; set; }

        public int OrderItemId { get; set; }

        public string? ItemName { get; set; }

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }
    }
}
