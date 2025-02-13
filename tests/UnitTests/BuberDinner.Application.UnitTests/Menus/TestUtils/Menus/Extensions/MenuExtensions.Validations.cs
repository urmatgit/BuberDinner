using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.MenuAggregate;

using FluentAssertions;

namespace BuberDinner.Application.UnitTests.Menus.TestUtils.Menus.Extensions
{
    public static partial class MenuExtensions
    {
        public static void ValidateCreateFrom(this  Menu menu,CreateMenuCommand command)
        {
            menu.Name.Should().Be(command.Name);
            menu.Description.Should().Be(command.Description);  
            menu.Sections.Should().HaveSameCount(command.Sections);
            menu.Sections.Zip(command.Sections).ToList().ForEach(pair=>ValidateSection(pair.First,pair.Second));

        }

        static void ValidateSection(MenuSection section, CreateMenuSectionCommand command) {
            section.Id.Should().NotBeNull();
            section.Name.Should().Be(command.Name);
            section.Description.Should().Be(command.Description);
            section.Items.Should().HaveSameCount(command.Items);
            section.Items.Zip(command.Items).ToList().ForEach(pair=>ValidateItem(pair.First, pair.Second));
        }

        private static void ValidateItem(MenuItem first, CreateMenuItemCommand second)
        {
            first.Id.Should().NotBeNull();
            first.Name.Should().Be(second.Name);    
            first.Description.Should().Be(second.Description);
        }
    }
}
