using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyPharmacy.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product is required")]
        public int OrderProductId { get; set; }
        [ForeignKey(nameof(OrderProductId))]
        [ValidateNever] 
        public OrderProduct OrderProduct { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        [ValidateNever] 
        public Client Client { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Status")]
        [ValidateNever]
        public int Status { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Updated { get; set; }
    }
}
