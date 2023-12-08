using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    public class CoinBox
    {
        private List<Coin> box;

        public CoinBox()
        {
            box = new List<Coin>();
        }

        public void AddCoin(Coin c)
        {
            box.Add(c);
        }

        public decimal GetValue()
        {
            decimal total = 0;
            foreach (Coin c in box) total += c.GetValue();
            return total;
        }

        // return total value???
        public void RemoveAllCoins()
        {
            box.Clear();
        }

        public decimal RemovePartial(decimal amount) // removes the amount possible and returns what must be taken from ChangeBox
        {
            box = box.OrderByDescending(coin => coin.GetValue()).ToList();
            int elementToAdd = 0;
            while (amount != 0M)
            {
                if (elementToAdd + 1 == box.Count) return amount;
                decimal working = box[elementToAdd].GetValue();
                if (working > amount)
                {
                    elementToAdd++;
                    Console.WriteLine("Index increasing {0} > {1}", working, amount);

                }
                else
                {
                    amount -= working;
                    box.RemoveAt(elementToAdd);
                }
                Console.WriteLine("amount = {0}", amount);
            }
            return amount;
        }

        public static decimal RemoveRest(decimal amount)
        {
            decimal takenAsChange = amount;
            int elementToAdd = 0;
            while (amount != 0M)
            {
                if (elementToAdd + 1 == Coin.coinArray.Length) return amount;
                decimal working = Coin.coinArray[elementToAdd].GetValue();
                if (working > amount)
                {
                    elementToAdd++;
                }
                else
                {
                    amount -= working;
                }
            }
            return takenAsChange;
        }
    }
}
