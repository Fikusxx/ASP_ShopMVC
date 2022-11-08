using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrialMVC.ViewModels
{
	public class DeleteOrderViewModel
	{
        public int Id { get; set; }
        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public int? ProviderId { get; set; }

        public SelectList? ProvidersSelectList { get; set; }
    }
}
