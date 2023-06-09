﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly Tufesa_Dev_Test.Data.TufesaDbContext _context;

        public DetailsModel(Tufesa_Dev_Test.Data.TufesaDbContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var Customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (Customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = Customer;
            }
            return Page();
        }
    }
}
