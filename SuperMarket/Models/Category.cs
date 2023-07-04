using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMarket.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
