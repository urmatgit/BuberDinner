using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.TestUtils.Constants;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils
{
    public static class CreateMenuCommandUtils
    {
        public static CreateMenuCommand CreateCommand(List<CreateMenuSectionCommand> sections=null)
        {
            return new CreateMenuCommand(
                  Constants.Host.Id.ToString()!,
                  Constants.Menu.Name,
                  Constants.Menu.Description,
                  5,
                  sections?? CreateMenuSectionCommand(10)
                );
        }
        public static List<CreateMenuSectionCommand> CreateMenuSectionCommand(int sectionCount, List<CreateMenuItemCommand> items=null) =>
            Enumerable.Range(0, sectionCount)
                .Select(index => new CreateMenuSectionCommand(
                    Constants.Menu.SectionNameFromIndex(index),
                    Constants.Menu.SectionDescriptionFromIndex(index),
                    items??CreateMenuItemCommand(10)
                    ))
               .ToList();
        public static List<CreateMenuItemCommand> CreateMenuItemCommand(int index)
            => Enumerable.Range(0, index)
            .Select(index => new CreateMenuItemCommand(
                Constants.Menu.ItemNameFromIndex(index),
                Constants.Menu.ItemDescriptionFromIndex(index)
                )).ToList();
    }
}
