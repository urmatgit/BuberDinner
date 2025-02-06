using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Price
{
    public class PriceMenu
    {
        public double Amount { get; private set; }
        public string Currency { get; private set; }
        private PriceMenu(double amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static PriceMenu Create(double amount, string currency)
        {
            return new PriceMenu(amount, currency);
        }
    }
}
