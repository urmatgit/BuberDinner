using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Reservation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Reservation
{
    public class Reservation : AggregateRoot<ReservationId,Guid>
    {
        private Reservation(ReservationId id) : base(id) { }
        public static Reservation Create()
        {
            return new Reservation(ReservationId.CreateUnique())
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };
        }

       

        public ReservationId Id { get; set; }
        public int QuestCount { get; set; }
        public string ReservationStatus { get; set; }
        public GuestId GuestId { get; set; }
        public BillId BillId { get; set; }
        public DinnerId DinnerId { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }




    }
}
