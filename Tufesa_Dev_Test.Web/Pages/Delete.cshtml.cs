using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tufesa_Dev_Test.Data;
using Tufesa_Dev_Test.Data.Models;

namespace Tufesa_Dev_Test.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Tufesa_Dev_Test.Data.TufesaDbContext _context;

        public DeleteModel(Tufesa_Dev_Test.Data.TufesaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            this.Customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var Customer = await _context.Customers.FindAsync(id);

            if (Customer != null)
            {
                Customer = Customer;
                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
