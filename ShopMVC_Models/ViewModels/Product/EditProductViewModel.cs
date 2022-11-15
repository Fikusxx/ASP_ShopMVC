using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_ViewModels
{
	public class EditProductViewModel
	{
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public IFormFile? Image { get; set; }
        public string? PhotoPath { get; set; }

        [Required(ErrorMessage = "Please select category type")]
        public int CategoryId { get; set; }

        public SelectList? CategorySelectList { get; set; }

        [Required(ErrorMessage = "Please select application type")]
        public int ApplicationTypeId { get; set; }

        public SelectList? ApplicationTypesSelectList { get; set; }
    }
}
