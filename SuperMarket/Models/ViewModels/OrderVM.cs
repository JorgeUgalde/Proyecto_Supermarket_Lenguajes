using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SuperMarket.Models.ViewModels
{
    public class OrderVM
    {
        [ValidateNever]
        public Order Order { get; set; }

        [ValidateNever]
        public IEnumerable<ProductOrder> ProductOrder { get; set; }
        
        
        [ValidateNever]
        public IEnumerable<Product> SelectedProducts { get; set; }

       
    }
}
