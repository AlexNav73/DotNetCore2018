using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Core.Breadcrumbs;
using DotNetCore2018.WebApi.Controllers;

using DbSupplier = DotNetCore2018.Data.Entities.Supplier;

namespace DotNetCore2018.WebApi.Pages.Supplier
{
    [Breadcrumb("Edit", Parent = typeof(SupplierController))]
    public class EditModel : PageModel
    {
        private readonly IDataService _dataService;

        [BindProperty]
        public DbSupplier Supplier { get; set; }

        public EditModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult OnGet(int id)
        {
            var supplier = _dataService.GetBy(new IdSpecification<DbSupplier>(id));
            if (supplier == null)
            {
                return RedirectToAction("Index", "Supplier");
            }
            Supplier = supplier;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _dataService.Update(Supplier);
                return RedirectToAction("Index", "Supplier");
            }
            return Page();
        }
    }
}