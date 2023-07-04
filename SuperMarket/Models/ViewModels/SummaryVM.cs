using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SuperMarket.Models.ViewModels
{
    public class SummaryVM
    {

        [ValidateNever]
        public IEnumerable<Product> AllProducts { get; set; }

        [ValidateNever]
        public IEnumerable<Category> AllCategories { get; set; }

        [ValidateNever]
        public IEnumerable<Order> AllOrders { get; set; }

        [ValidateNever]
        public Store Store { get; set; }
    }
}
