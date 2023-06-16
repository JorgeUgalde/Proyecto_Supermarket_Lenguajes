using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public IEnumerable<ProductOrder> ProductOrder { get; set; }

    }
}
