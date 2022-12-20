using System.ComponentModel.DataAnnotations;

namespace MyPharmacy.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Category name")]
        [Required]
        public string Name { get; set; }
    }
}
