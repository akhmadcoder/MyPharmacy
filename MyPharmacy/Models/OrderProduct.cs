using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyPharmacy.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
