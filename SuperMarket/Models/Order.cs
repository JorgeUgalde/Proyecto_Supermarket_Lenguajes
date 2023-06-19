using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMarket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }

        [NotMapped]
        public IEnumerable<ProductOrder> ProductOrder { get; set; }

    }
}
