using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Price;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Bill
{
    public class Bill: AggregateRoot<BillId,Guid>
    {
        private Bill(BillId id, DinnerId dinnerId,GuestId guestId,  HostId hostId, PriceMenu price) : base(id)
        {
            Id = id;
            dinnerId = dinnerId;
            HostId = hostId;
            GuestId = guestId;
            Price = price;    


        }
        public static Bill Create( DinnerId dinnerId,GuestId guestId,  HostId hostId, PriceMenu price)
        {
            return new Bill(BillId.CreateUnique(), dinnerId,guestId, hostId, price)
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };
        }
        public BillId Id { get; set; }
            public DinnerId DinnerId { get; set; }
            public GuestId GuestId { get; set; }
            public HostId HostId { get; set; }
            public PriceMenu Price { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public DateTime UpdatedDateTime { get; set; }

#pragma warning disable CS8618
        private Bill()
        {

        }
#pragma warning restore CS8618

    }
}
