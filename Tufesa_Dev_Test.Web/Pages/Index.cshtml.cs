using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tufesa_Dev_Test.Data;
using Tufesa_Dev_Test.Data.Models;
using Tufesa_Dev_Test.Core.Interfaces;

namespace Tufesa_Dev_Test.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _api;

        public IndexModel(ICustomerRepository context)
        {
            _api = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                var actionResult = await _api.ReadAll();
                var objectResult = actionResult as ObjectResult;
                if (objectResult.StatusCode != 200)
                    RedirectToAction("Error");
                var value = objectResult.Value;
                Customer = (IList<Customer>)value;

            }
            catch
            {
                RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var actionResult = await _api.Delete(id);
            var objectResult = actionResult as ObjectResult;
            if (objectResult.StatusCode != 200)
            {
                return Page();
            }
            return new OkResult();
        }
    }
}
