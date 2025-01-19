using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PhoneUserKICB.PL.Models.ViewModels
{

    /// <summary>
    /// View model for user
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailUnique", "Users", ErrorMessage = "Email already exists.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UserViewModel), "ValidateDateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth > DateTime.Now)
            {
                return new ValidationResult("Date of birth cannot be future.");
            }
            return ValidationResult.Success;
        }
    }
}
