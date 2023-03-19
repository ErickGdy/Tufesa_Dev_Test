using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tufesa_Dev_Test.Data;
using Tufesa_Dev_Test.Data.Models;
using Microsoft.Extensions.Options;
using Tufesa_Dev_Test.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Tufesa_Dev_Test.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _api;

        public List<SelectListItem> Options { get; set; }
        public int? _id;


        public EditModel(ICustomerRepository context)
        {
            _api = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _id = id;
            var actionResult = await _api.Read(id.ToString());
            var objectResult = actionResult as ObjectResult;
            var value = objectResult.Value;
            if (objectResult.StatusCode != 200)
            {
                ModelState.AddModelError("Customer", value.ToString());
                return Page();

            }
            Customer = (Customer)value;
            if (Customer == null)
            {
                return NotFound();
            }
            Options = new List<SelectListItem>();
            Options.Add(new SelectListItem() { Value = "0", Text = "Not Set", 
                Selected = Customer.Status == Customer.CustomerStatus.NotSet ? true : false });
            Options.Add(new SelectListItem() { Value = "1", Text = "Active",
                Selected = Customer.Status == Customer.CustomerStatus.Active ? true : false });
            Options.Add(new SelectListItem() { Value = "2", Text = "Inactive",
                Selected = Customer.Status == Customer.CustomerStatus.Inactive ? true : false });

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Options = new List<SelectListItem>();
            Options.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Not Set",
                Selected = Customer.Status == Customer.CustomerStatus.NotSet ? true : false
            });
            Options.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Active",
                Selected = Customer.Status == Customer.CustomerStatus.Active ? true : false
            });
            Options.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Inactive",
                Selected = Customer.Status == Customer.CustomerStatus.Inactive ? true : false
            });
            if (!ModelState.IsValid || Customer == null)
            {
                return Page();
            }
            var actionResult = await _api.Update(Customer.Id, Customer);
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
