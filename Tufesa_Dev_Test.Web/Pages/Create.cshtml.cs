using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tufesa_Dev_Test.Data;
using Tufesa_Dev_Test.Data.Models;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using Tufesa_Dev_Test.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tufesa_Dev_Test.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _api;

        public List<SelectListItem> Options { get; set; }


        public CreateModel(ICustomerRepository context)
        {
            _api = context;
        }

        public IActionResult OnGet()
        {
            Options = new List<SelectListItem>();
            Options.Add(new SelectListItem() { Value = "0", Text = "Not Set" });
            Options.Add(new SelectListItem() { Value = "1", Text = "Active", Selected = true });
            Options.Add(new SelectListItem() { Value = "2", Text = "Inactive" });
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Options = new List<SelectListItem>();
            Options.Add(new SelectListItem() { Value = "0", Text = "Not Set" });
            Options.Add(new SelectListItem() { Value = "1", Text = "Active" });
            Options.Add(new SelectListItem() { Value = "2", Text = "Inactive" });
            if (!ModelState.IsValid ||  Customer == null)
            {
                return Page();
            }
            var actionResult = await _api.Create(Customer);
            var objectResult = actionResult as ObjectResult;
            if (objectResult.StatusCode != 200)
            {
                var value = objectResult.Value;
                ModelState.AddModelError("Customer", value.ToString());
                return Page();

            }
            return new OkResult();
        }
    }
}
