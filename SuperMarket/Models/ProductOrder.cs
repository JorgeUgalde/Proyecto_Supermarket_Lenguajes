using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMarket.Models
{
    public class ProductOrder
    {
        [DisplayName("Product")]
        public int IdProduct { get; set; }
        [DisplayName("Order")]
        public int IdOrder { get; set; }

        [Required]
        [ForeignKey("IdProduct")]
        public Product Product { get; set; }
        [Required]
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

    }
}
