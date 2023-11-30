using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    class VendingMachine
    {
        private List<Product> products;
        public CoinBox coins;
        public CoinBox currentCoins;

        public VendingMachine()
        {
            products = new List<Product>();
            coins = new CoinBox();
            currentCoins = new CoinBox();
        }

        public List<Product> getProductTypes()
        {
            List<Product> types = new List<Product>();
            for (int i = 0; i < products.Count; i++)
            {
                Product entry = products[i];
                types.Add(entry);
            }
            return types;
        }

        public void addProduct(Product p)
        {
            products.Add(p);
        }

        public double addCoin(Coin c)
        {
            currentCoins.addCoin(c);
            return currentCoins.getValue();
        }

        public string buyProduct(Product p)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Product prod = products[i];
                if (prod == p)
                {
                    double payment = currentCoins.getValue();

                    if (prod.Quantity == 0) { return string.Format("Out of Product {0}", prod.getDescription()); }

                    else if (p.getPrice() <= payment)
                    {
                        if (products[i].Quantity > 1)
                        {
                            products[i].Quantity--;
                        }
                        else
                        {
                            products.RemoveAt(i);
                        }
                        coins.addCoins(currentCoins);
                        currentCoins.removeAllCoins();
                        return "OK";
                    }
                    else
                    {
                        return "Insufficient money";
                    }
                }
            }
            return "No such product";
        }

        public bool productsAvailable()
        {
            return products.Count > 0;
        }

        public double removeMoney()
        {
            double r = coins.getValue();
            coins.removeAllCoins();
            return r;
        }
    }
}
