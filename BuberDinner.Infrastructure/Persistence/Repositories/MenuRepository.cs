using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Menu;

namespace BuberDinner.Infrastructure.Persistence.Repositories
{
    public class MenuRepository : IMenuRepository
    {

        private readonly BuberDinnerDbContext _dbContext;
        public MenuRepository(BuberDinnerDbContext buberDinnerDb)
        {
            _dbContext = buberDinnerDb;
        }

        public void Add(Menu menu)
        {
            _dbContext.Add(menu);
            _dbContext.SaveChanges();
        }
    }
}
