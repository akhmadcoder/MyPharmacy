using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPharmacy.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [NotMapped]
        public string SelectedRole { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string FirstPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("FirstPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

    }
}
