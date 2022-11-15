using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_ViewModels
{
	public class CreateProductViewModel
	{
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select category type")]
        public int CategoryId { get; set; }

        public SelectList? CategorySelectList { get; set; }

        [Required(ErrorMessage = "Please select application type")]
        public int ApplicationTypeId { get; set; }

        public SelectList? ApplicationTypesSelectList { get; set; }
    }
}
