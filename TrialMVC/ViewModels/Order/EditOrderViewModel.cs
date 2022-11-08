using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TrialMVC.ViewModels
{
	public class EditOrderViewModel
	{
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Invalid order provider")]
        public int ProviderId { get; set; }

        public SelectList? ProvidersSelectList { get; set; } = null!;
    }
}
