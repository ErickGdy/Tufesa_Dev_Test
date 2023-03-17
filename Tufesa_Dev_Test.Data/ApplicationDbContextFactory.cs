using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Tufesa_Dev_Test.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<TufesaDbContext>
    {
        public TufesaDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var optionsBuilder = new DbContextOptionsBuilder<TufesaDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new TufesaDbContext(optionsBuilder.Options);
        }
    }
}
