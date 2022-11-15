using System.ComponentModel.DataAnnotations;

namespace ShopMVC_Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Display Order is not valid")]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order must be greater than 0")]
        public int DisplayOrder { get; set; }
    }
}
