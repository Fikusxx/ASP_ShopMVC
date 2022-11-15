using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_ViewModels
{
	public class DeleteProductViewModel
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public string? ShortDescription { get; set; }

        public int Price { get; set; }

        public string? PhotoPath { get; set; }

        public int CategoryId { get; set; }

        public SelectList? CategorySelectList { get; set; }

        public int ApplicationTypeId { get; set; }

        public SelectList? ApplicationTypesSelectList { get; set; }
    }
}
