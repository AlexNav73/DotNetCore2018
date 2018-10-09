using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;

using DbProduct = DotNetCore2018.Data.Entities.Product;
using DbCategory = DotNetCore2018.Data.Entities.Category;
using DbSupplier = DotNetCore2018.Data.Entities.Supplier;

namespace DotNetCore2018.WebApi.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IDataService _dataService;

        [BindProperty]
        public DbProduct Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public EditModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult OnGet(int id)
        {
            var product = _dataService.GetBy(new IdSpecification<DbProduct>(id));
            if (product == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Categories = _dataService.GetAll<DbCategory>()
                    .Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    })
                    .ToArray();
            Suppliers = _dataService.GetAll<DbSupplier>()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToArray();
            Product = product;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _dataService.Update(Product);
                return RedirectToAction("Index", "Product");
            }
            return Page();
        }
    }
}