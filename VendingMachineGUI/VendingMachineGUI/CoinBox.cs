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

        /// <summary>
        /// Constructor that initializes the list of coins
        /// </summary>
        public CoinBox()
        {
            box = new List<Coin>();
        }

        /// <summary>
        /// Adds a coin to the coin box
        /// </summary>
        /// <param name="c"> the coin </param>
        public void AddCoin(Coin c)
        {
            box.Add(c);
        }

        /// <summary>
        /// Gets the total of all of the coins' values in the box
        /// </summary>
        /// <returns> the total </returns>
        public decimal GetValue()
        {
            decimal total = 0;
            foreach (Coin c in box) total += c.GetValue();
            return total;
        }

        /// <summary>
        /// Removes all the coins from the box
        /// </summary>
        public void RemoveAllCoins()
        {
            box.Clear();
        }

        /// <summary>
        /// Removes the amount possible and returns what must be taken from ChangeBox
        /// </summary>
        /// <param name="amount"> the amount possible </param>
        /// <returns> what must be taken from ChangeBox </returns>
        public decimal RemovePartial(decimal amount)
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

        /// <summary>
        /// Removes the rest of the changeBox 
        /// </summary>
        /// <param name="amount"> the amount taken as change </param>
        /// <returns> what was taken as change </returns>
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
