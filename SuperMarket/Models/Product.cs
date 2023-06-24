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
        public string BarCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required]
        [Range(0, 1)]
        public int IsActive { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Unit { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
