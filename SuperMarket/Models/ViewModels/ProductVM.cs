using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.Models;

namespace SuperMarket.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }

        public ProductVM()
        {
            SelectedCategories = new List<int>();
        }
    }
}