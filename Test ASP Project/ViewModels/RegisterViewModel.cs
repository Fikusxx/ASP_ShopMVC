using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Test_ASP_Project.Utilities;

namespace Test_ASP_Project.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"Account")]
        [ValidEmailDomain(allowedDomain: "test.com", ErrorMessage = "Email is not in test.com domain")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? City { get; set; }
    }
}
