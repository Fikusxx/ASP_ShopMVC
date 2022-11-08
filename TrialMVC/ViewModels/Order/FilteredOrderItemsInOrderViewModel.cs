using Microsoft.AspNetCore.Mvc.Rendering;
using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.ViewModels
{
	public class FilteredOrderItemsInOrderViewModel
	{
        public int Id { get; set; }
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int? ProviderId { get; set; }
        public List<string>? OrderItemsNameList { get; set; } = new();

        public List<string>? OrderItemsUnitList { get; set; } = new();

        public SelectList? ProvidersSelectList { get; set; } = null!;
        public SelectList? OrderItemsNameSelectList { get; set; } = null!;
        public SelectList? OrderItemsUnitSelectList { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
