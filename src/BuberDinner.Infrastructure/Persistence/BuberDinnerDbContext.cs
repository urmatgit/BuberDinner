using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Infrastructure.Persistence.Interceptors;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence
{
    public class BuberDinnerDbContext: DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<Dinner> Dinner { get; set; } = null!;
        public BuberDinnerDbContext(DbContextOptions options,PublishDomainEventsInterceptor publishDomainEventsInterceptor): base(options) { 
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
