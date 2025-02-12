using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.Entities;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
    {
        private readonly IMenuRepository _menuRepository;
        public CreateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // create menu
            var menu=Menu.Create(
                request.Name,
                request.Description,
                request.AverageRating,
                HostId.Create(request.HostId),
                request.Sections.ConvertAll(sections => MenuSection.Create(
                                sections.Name, 
                                sections.Description,
                                sections.Items.ConvertAll(item =>
                                    MenuItem.Create(item.Name, item.Description)))
                ));
            //persist menu
            //return menu;
            _menuRepository.Add(menu);
            return menu;
        }
    }
}
