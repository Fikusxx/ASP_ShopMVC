using Microsoft.AspNetCore.Mvc.Rendering;
using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int? ProviderId { get; set; }

        public SelectList? ProvidersSelectList { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
