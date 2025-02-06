using BuberDinner.Domain.Common;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.Price;
using BuberDinner.Domain.Reservation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Dinner
{
    public class Dinner : AggregateRoot<DinnerId>
    {

        private Dinner(DinnerId dinnerId): base(dinnerId) { }
        public static Dinner Create()
        {
            return new Dinner(DinnerId.CreateUnique())
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };
        }
             
         
            public DinnerId Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public DateTime? StartedDateTime { get; set; }
            public DateTime? EndedDateTime { get; set; }
            public string Status { get; set; }
            public bool IsPublic { get; set; }
            public int MaxGuests { get; set; }
            public PriceMenu Price { get; set; }
            public HostId HostId { get; set; }
            public MenuId MenuId { get; set; }
            public string ImageUrl { get; set; }
            public Location Location { get; set; }
            public List<ReservationId> Reservations { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public DateTime UpdatedDateTime { get; set; }
        }

        

    
}
