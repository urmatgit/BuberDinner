using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Entities
{
    public class User: AggregateRoot<UserId,Guid>
    {
        private User(UserId id,string firstName,string lastName,string eMail,string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = eMail;
            Password = password;

        }
        public static User Create(string firstName, string lastName, string eMail, string password)
        { 
            return new User(UserId.CreateUnique(), firstName, lastName, eMail, password);
        }
            
            
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
