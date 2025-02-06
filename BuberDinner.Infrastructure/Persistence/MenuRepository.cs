using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Menu;

namespace BuberDinner.Infrastructure.Persistence
{
    public class MenuRepository : IMenuRepository
    {
        private static readonly List<Menu> _menuList = new List<Menu>();
        public void Add(Menu menu)
        {
            _menuList.Add(menu);
        }
    }
}
