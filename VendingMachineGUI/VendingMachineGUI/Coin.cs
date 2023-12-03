using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    public class Coin
    {
        private double value;
        private string name;
        public static Coin NICKEL = new Coin(.05, "nickel");
        public static Coin DIME = new Coin(.10, "dime");
        public static Coin QUARTER = new Coin(.25, "quarter");
        public static Coin HALFDOLLAR = new Coin(.50, "half dollar");
        public static Coin DOLLAR = new Coin(1.0, "dollar");
        public static Coin FIVER = new Coin(5.0, "five dollar bill");

        public Coin(double aValue, string aName)
        {
            value = aValue;
            name = aName;
        }
        public double getValue()
        {
            return value;
        }
        public string getName()
        {
            return name;
        }
    }
}
