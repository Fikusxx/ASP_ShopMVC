using System.ComponentModel.DataAnnotations;


namespace ShopMVC_Models
{
    public class ApplicationType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
