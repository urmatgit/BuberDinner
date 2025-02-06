using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Entities.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Guest
{
    public class Guest : AggregateRoot<GuestId>
    {
        private Guest(GuestId id) : base(id)
        {
        }
        public static Guest Creat()
        {
            return new Guest(GuestId.CreateUnique())
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };

        }
        public GuestId Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfileImage { get; set; }
            public float AverageRating { get; set; }
            public UserId UserId { get; set; }
            public List<DinnerId> UpcomingDinnerIds { get; set; }
            public List<DinnerId> PastDinnerIds { get; set; }
            public List<DinnerId> PendingDinnerIds { get; set; }
            public List<BillId> BillIds { get; set; }
            public List<MenuReviewId> MenuReviewIds { get; set; }
            public List<RatingId> Ratings { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public DateTime UpdatedDateTime { get; set; }
        }

        

    
}
