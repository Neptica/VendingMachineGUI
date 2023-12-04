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

        public Coin(decimal aValue, string aName)
        {
            value = aValue;
            name = aName;
        }
        public decimal getValue()
        {
            return value;
        }
        public string getName()
        {
            return name;
        }
    }
}
