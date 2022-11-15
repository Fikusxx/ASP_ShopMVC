using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC_Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public string PhotoPath { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ApplicationTypeId { get; set; }
        public ApplicationType ApplicationType { get; set; }

        [NotMapped]
        [Range(1, 10000, ErrorMessage = "Quantity must be greater than 0")]
        public int TempSqft { get; set; } = 1;
    }
}
