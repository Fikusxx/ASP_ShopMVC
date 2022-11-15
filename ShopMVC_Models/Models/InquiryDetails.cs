using System.ComponentModel.DataAnnotations;


namespace ShopMVC_Models
{
	public class InquiryDetails
	{
		public int Id { get; set; }

        [Required]
        public int InquiryHeaderId { get; set; }

        public InquiryHeader InquiryHeader { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
