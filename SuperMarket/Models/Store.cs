using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public int IsOpen { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
