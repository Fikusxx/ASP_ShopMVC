using Microsoft.AspNetCore.Mvc.Rendering;


namespace ShopMVC_Models
{
    public class OrderListViewModel
    {
        public List<OrderHeader> OrderHeadersList { get; set; }
        public SelectList StatusList { get; set; }
        public string Status { get; set; }
    }
}


