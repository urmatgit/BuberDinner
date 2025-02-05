using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu
{
    public sealed class Menu: AggregateRoot<MenuId>
    {
        private readonly List<MenuSection> _sections = new List<MenuSection>();
        private readonly List<DinnerId> _dinnerIds = new List<DinnerId>();
        private readonly List<MenuReviewId> _menuReviewIds = new List<MenuReviewId>();
        public string Name { get;  }
        public string Description { get; }
        public float AverageRating { get; }
        
        
        

        public HostId HostId { get; }
        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public DateTime CreateDateTime { get; }
        public DateTime UpdateDateTime { get; }

        private Menu(MenuId menuId,string name,string description,HostId hostId,  DateTime createDateTime, DateTime updateDateTime): base(menuId)
        {
            Name = name;
            Description = description;
            HostId = hostId;
            CreateDateTime = createDateTime;
                
            UpdateDateTime = updateDateTime;
        }
        public static Menu Create(string name, string description, HostId hostId) 
            => new Menu(MenuId.CreateUnique(), name, description, hostId, DateTime.UtcNow, DateTime.UtcNow);
    }
}
