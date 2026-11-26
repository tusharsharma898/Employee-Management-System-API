using System.ComponentModel.DataAnnotations;

namespace Employee_API_JWT.Models.ViewModels
{
    public class ProfileVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}