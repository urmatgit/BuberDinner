﻿using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using BuberDinner.Domain.Menu;

using Mapster;
using MenuSection = BuberDinner.Domain.Menu.Entities.MenuSection;
using MenuItem = BuberDinner.Domain.Menu.Entities.MenuItem;
using BuberDinner.Domain.MenuAggregate;
namespace BuberDinner.Api.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
                .Map(dest => dest.HostId, src => src.HostId)
                .Map(dest=>dest,srs=>srs.Request);
            config.NewConfig<Menu, MenuResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest=>dest.AverageRating,src=>src.AverageRating.Value)
                .Map(dest => dest.HostId, src => src.HostId.Value)
                .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(d=>d.Value))
                .Map(dest => dest.MunuReviewIds, src => src.MenuReviewIds.Select(d => d.Value))
                ;
            config.NewConfig<MenuSection,MenuSectionResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
            config.NewConfig<MenuItem, MenuItemResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
