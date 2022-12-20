using System.ComponentModel.DataAnnotations;

namespace MyPharmacy.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Client name")]
        [Required(ErrorMessage = "Client Name is required")]
        public string Name { get; set; }

        [Display(Name = "Client email")]
        [Required(ErrorMessage = "Client email is required")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }

        [Display(Name = "Client address")]
        [Required(ErrorMessage = "Client address is required")]
        public string Address { get; set; }
    }
}
