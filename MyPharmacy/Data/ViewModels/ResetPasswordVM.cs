using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPharmacy.Data.ViewModels
{
    public class ResetPasswordVM
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "CurrentPassword is required.")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password is required.")]
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
