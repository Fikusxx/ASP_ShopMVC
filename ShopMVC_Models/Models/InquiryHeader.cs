﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ShopMVC_Models
{
	public class InquiryHeader
	{
		public int Id { get; set; }

		public string ApplicationUserId { get; set; }

		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		public DateTime InquiryDate { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
