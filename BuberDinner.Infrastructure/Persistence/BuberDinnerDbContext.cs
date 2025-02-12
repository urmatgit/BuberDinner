using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Menu;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence
{
    public class BuberDinnerDbContext: DbContext
    {
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<Dinner> Dinner { get; set; } = null!;
        public BuberDinnerDbContext(DbContextOptions options): base(options) { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
