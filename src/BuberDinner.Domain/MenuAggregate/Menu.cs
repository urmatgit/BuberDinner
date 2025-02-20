﻿using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuAggregate.Events;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.MenuAggregate
{
    public sealed class Menu: AggregateRoot<MenuId,Guid>
    {
        private readonly List<MenuSection> _sections = new List<MenuSection>();
        private readonly List<DinnerId> _dinnerIds = new List<DinnerId>();
        private readonly List<MenuReviewId> _menuReviewIds = new List<MenuReviewId>();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AverageRating AverageRating { get; private set; }
        
        
        

        public HostId HostId { get; private set; }
        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public DateTime CreateDateTime { get; private set; }
        public DateTime UpdateDateTime { get; private set; }
        
        private Menu(MenuId menuId,string name,string description,double rating, HostId hostId,List<MenuSection>? menuSections,  DateTime createDateTime, DateTime updateDateTime): base(menuId)
        {
            Name = name;
            Description = description;
            this.AverageRating = AverageRating.CreateNew(rating,(int)rating);
             HostId = hostId;
            _sections = menuSections;
            CreateDateTime = createDateTime;
                
            UpdateDateTime = updateDateTime;
        }
        public static Menu Create(string name, string description, double rating, HostId hostId,List<MenuSection>? menuSections)
        {
            var menu=new Menu(MenuId.CreateUnique(), name, description, rating, hostId, menuSections, DateTime.UtcNow, DateTime.UtcNow);
            menu.AddDomainEvent(new MenuCreated(menu));
            return menu;
        }
            
#pragma warning disable CS8618
        private Menu()
        {

        }
#pragma warning restore CS8618
    }
}
