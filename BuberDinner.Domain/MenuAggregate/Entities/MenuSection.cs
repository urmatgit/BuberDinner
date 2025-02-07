﻿using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuSection: Entity<MenuSectionId>
    {

        private readonly List<MenuItem> _items = new();
        public string Name { get; }
        public string Description { get; }

        public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();
        private MenuSection(MenuSectionId menuSectionId, string name, string description,List<MenuItem> menuItems) : base(menuSectionId) { 
            Name = name;
            Description = description;
            _items = menuItems;
        }
        public static MenuSection Create(string name, string description,List<MenuItem>? menuItems)
            =>new MenuSection(MenuSectionId.CreateUnique(),name, description,menuItems);
#pragma warning disable CS8618
        private MenuSection() { }
#pragma warning restore CS8618
    }
}
