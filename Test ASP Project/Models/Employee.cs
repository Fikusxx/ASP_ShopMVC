using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test_ASP_Project.Models.Enums;

namespace Test_ASP_Project.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть 2 символов")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Емейл")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrent Email Format")]
        public string Email { get; set; }

        public Departments? Department { get; set; }
        public string? PhotoPath { get; set; }
    }
}
