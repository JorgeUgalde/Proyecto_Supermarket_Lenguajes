﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperMarket.Models.ViewModels
{
    public class ProductVM
    {
        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}