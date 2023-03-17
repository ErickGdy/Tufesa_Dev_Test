
using Tufesa_Dev_Test.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tufesa_Dev_Test.Data
{
    public class TufesaDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public TufesaDbContext(DbContextOptions<TufesaDbContext> options)
            : base(options)
        {
        }

    }
}
