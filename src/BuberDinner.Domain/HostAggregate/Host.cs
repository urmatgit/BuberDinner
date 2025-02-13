using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Entities.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Host
{
    public class Host : AggregateRoot<HostId,string>
    {

        private Host(HostId id) : base(id)
        {

        }
        public static Host Create()
        {
            return new Host(HostId.CreateUnique());
        }
        public class Rootobject
        {
            public HostId id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfileImage { get; set; }
            public float AverageRating { get; set; }
            public UserId UserId { get; set; }
            public List<MenuId> MenuIds { get; set; }
            public List<DinnerId> DinnerIds { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public DateTime UpdatedDateTime { get; set; }
        }

        

    }
}
