using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC_Models
{
    public class OrderViewModel
    {
        [Required]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public List<OrderDetails> OrderDetailsList { get; set; }
    }
}
