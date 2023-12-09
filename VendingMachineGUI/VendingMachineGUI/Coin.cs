using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    public class Coin
    {
        private decimal value;
        private string name;
        public static Coin NICKEL = new Coin(.05M, "nickel");
        public static Coin DIME = new Coin(.10M, "dime");
        public static Coin QUARTER = new Coin(.25M, "quarter");
        public static Coin HALFDOLLAR = new Coin(.50M, "half dollar");
        public static Coin DOLLAR = new Coin(1.0M, "dollar");
        public static Coin FIVER = new Coin(5.0M, "five dollar bill");
        public static Coin[] coinArray = { DOLLAR, HALFDOLLAR, QUARTER, DIME, NICKEL };

        /// <summary>
        /// Contructor that initializes the coin name and value
        /// </summary>
        /// <param name="aValue"> the value of the coin (.05 for nickel, 1 for dollar, etc.) </param>
        /// <param name="aName"> the name of the coin </param>
        public Coin(decimal aValue, string aName)
        {
            value = aValue;
            name = aName;
        }

        /// <summary>
        /// Gets the value of the desired coin
        /// </summary>
        /// <returns> the value of the desired coin </returns>
        public decimal GetValue()
        {
            return value;
        }
    }
}
