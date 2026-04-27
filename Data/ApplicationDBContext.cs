using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace receiptor.NET.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {}

        public DbSet<Models.Receipt> Receipts { get; set; }
        public DbSet<Models.Ingredient> Ingredients { get; set; }
    }
}