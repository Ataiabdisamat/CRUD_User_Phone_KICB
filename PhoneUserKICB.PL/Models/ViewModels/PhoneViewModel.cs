using System.ComponentModel.DataAnnotations;

namespace PhoneUserKICB.PL.Models.ViewModels
{
    public class PhoneViewModel
    {
        public int Id { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
