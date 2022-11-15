using ShopMVC_Models;
using System.ComponentModel.DataAnnotations;


namespace ShopMVC_ViewModels
{
	public class InquiryViewModel
	{
		[Required]
		public InquiryHeader InquiryHeader { get; set; }

		[Required]
		public List<InquiryDetails> InquiryDetailsList { get; set; }
	}
}
