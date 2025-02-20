﻿using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("hosts/{hostId}/menus")]
    public class MenusController :ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public MenusController(IMapper mapper,ISender sender)
        {
            _mapper = mapper;        
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenu(
            CreateMenuRequest request,
            string hostId)
        {
            var command = _mapper.Map<CreateMenuCommand>((request, hostId));
            var createMenuResult= await _sender.Send(command);
            return  createMenuResult.Match (
                menu =>Ok(_mapper.Map<MenuResponse>(menu)), // CreatedAtAction(nameof(GetMenu), new { hostId, menuId = menu.Id },menu),
                error=>Problem(error));
        }
    }
}
