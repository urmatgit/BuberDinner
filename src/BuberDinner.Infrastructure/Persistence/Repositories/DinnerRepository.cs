using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure.Persistence.Repositories
{
    public class DinnerRepository : IDinnerRepository
    {

        private readonly BuberDinnerDbContext _dbContext;
        public DinnerRepository(BuberDinnerDbContext buberDinnerDb)
        {
            _dbContext = buberDinnerDb;
        }

        public void Add(Dinner menu)
        {
            _dbContext.Add(menu);
            _dbContext.SaveChanges();
        }
    }
}
