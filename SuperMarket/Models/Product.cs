using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BarCode { get; set; }
        
        [Required]
        public string Name { get; set;}

        [Required]
        public int Price { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required]
        [Range(0,1)]
        public int IsActive { get; set; }

        
        public string PictureUrl { get; set; }

        [Required]
        public string Unit { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public IEnumerable<ProductOrder> ProductOrders { get; set;}

    }
}
