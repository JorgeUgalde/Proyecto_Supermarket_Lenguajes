using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SuperMarket.Models.ViewModels


{
    public class OrderDataVM
    {

        [ValidateNever]
        public IEnumerable<Dictionary<string, int>> ProductsData { get; set; }


        [ValidateNever]
        public int UserId { get; set; }

    }
}
