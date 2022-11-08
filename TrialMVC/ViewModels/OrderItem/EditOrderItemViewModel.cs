using System.ComponentModel.DataAnnotations;

namespace TrialMVC.ViewModels
{
	public class EditOrderItemViewModel
	{
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public int OrderItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Select quantity of items in order")]
        [Range(0, int.MaxValue)]
        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
