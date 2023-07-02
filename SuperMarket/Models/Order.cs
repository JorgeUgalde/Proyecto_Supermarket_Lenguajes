using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }

        [ForeignKey("ApplicationUserId")]
        public int ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
