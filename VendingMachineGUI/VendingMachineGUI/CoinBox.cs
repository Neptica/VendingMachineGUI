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

        public double getValue()
        {
            double total = 0;
            foreach (Coin c in box) total += c.getValue();
            return total;
        }

        // return total value???
        public void removeAllCoins()
        {
            box.Clear();
        }
    }
}
