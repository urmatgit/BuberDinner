using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Menu;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu
{
    public record CreateMenuCommand
     (
        string HostId,
         string Name,
         string Description,
         double AverageRating,
         List<MenuSectionCommand> Sections
     ) : IRequest<ErrorOr<Menu>>
    {

    }
    public record MenuSectionCommand(string Name, string Description, List<MenuItemCommand> Items);
    public record MenuItemCommand(string Name, string Description);
}
