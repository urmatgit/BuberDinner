using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuItem : AggregateRoot<MenuItemId>
    {
        private MenuItem(MenuItemId menuItemId,string name,string description): base(menuItemId)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; }
        public string Description { get; }
        public static MenuItem Create(string name,string description)
        {
            return new MenuItem(MenuItemId.CreateUnique(),name, description);
        }
#pragma warning disable CS8618
        private MenuItem() { }
#pragma warning restore CS8618
    }
}
