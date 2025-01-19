using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneUserKICB.PL.Models.ViewModels
{
    /// <summary>
    /// View model for phone
    /// </summary>
    public class PhoneViewModel
    {
        public int Id { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please enter correct phone number format")]    
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 13 characters.")]
        public string PhoneNumber { get; set; }

        [Required]
        public int UserId { get; set; }

        
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
