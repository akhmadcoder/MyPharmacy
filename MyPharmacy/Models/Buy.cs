using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MyPharmacy.Models
{
    public class Buy
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [ValidateNever]
        public Product Product { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        [ValidateNever]
        public Client Client { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Display(Name = "Item Price")]
        [Required(ErrorMessage = "Item Price is required")]
        public double ItemPrice { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost is required")]
        public double Cost { get; set; }

        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Updated { get; set; }



    }
}
