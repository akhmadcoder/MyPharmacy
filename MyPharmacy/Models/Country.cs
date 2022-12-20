using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyPharmacy.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Country name")]
        [Required]
        public string Name { get; set; }
    }
}
