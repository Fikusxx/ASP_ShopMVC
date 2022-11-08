using Microsoft.AspNetCore.Mvc.Rendering;
using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.ViewModels
{
	public class FilteredOrdersViewModel
	{
		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public List<string>? OrderNumberList { get; set; } = new();

		public List<int>? ProviderIdList { get; set; } = new();

		public List<string>? ProviderNameList { get; set; } = new();

		public SelectList? OrderNumberSelectList { get; set; } = null!;

        public SelectList? ProviderIdSelectList { get; set; } = null!;

        public SelectList? ProviderNameSelectList { get; set; } = null!;

        public List<Order>? OrderList { get; set; } = new(); 
	}
}
