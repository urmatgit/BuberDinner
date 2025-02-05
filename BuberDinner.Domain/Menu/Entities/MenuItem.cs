using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuItem : Entity<MenuItemId>
    {
        public MenuItem(MenuItemId menuItemId,string name,string description): base(menuItemId)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; }
        public string Description { get; }
        private static MenuItem Create(string name,string description)
        {
            return new MenuItem(MenuItemId.CreateUnique(),name, description);
        }
    }
}
