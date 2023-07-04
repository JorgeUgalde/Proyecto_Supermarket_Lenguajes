using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Barcode")]
        public string BarCode { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Quantity available")]
        public int InStock { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "Available to users")]
        public int IsActive { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public string PictureUrl { get; set; }

        [Required]
        [Display(Name = "Product measurement")]
        public string Unit { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
