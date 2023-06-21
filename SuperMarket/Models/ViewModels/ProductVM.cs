using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.Models;

namespace SuperMarket.Models.ViewModels
{
    public class ProductVM
    {
        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<int> SelectedCategories { get; set; }

    }
}