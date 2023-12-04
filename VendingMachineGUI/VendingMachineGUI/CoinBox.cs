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

        public void addCoin(Coin c)
        {
            box.Add(c);
        }

        public void addCoins(CoinBox other)
        {
            box.AddRange(other.box);
        }

        public decimal getValue()
        {
            decimal total = 0;
            foreach (Coin c in box) total += c.getValue();
            return total;
        }

        // return total value???
        public void removeAllCoins()
        {
            box.Clear();
        }

        public decimal removePartial(decimal amount) // removes the amount specificed in coins
        {
            box = box.OrderByDescending(coin => coin.getValue()).ToList();
            int elementToAdd = 0;
            while (amount != 0M)
            {
                Console.WriteLine(box.Count);
                decimal working = box[elementToAdd].getValue();
                if (working > amount)
                {
                    elementToAdd++;
                    Console.WriteLine("Index increasing {0} > {1}", working, amount);
                    if (elementToAdd + 1 == box.Count) return amount;
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
    }
}
