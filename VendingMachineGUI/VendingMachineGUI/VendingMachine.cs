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
            products = new List<Product>(){
            new Product("Cool Ranch Doritos", 1.25, 0),
            new Product("Doritos", 1.25, 0),
            new Product("Cheetos", 1.25, 0),
            new Product("Funyuns", 1.25, 0),
            new Product("Snickers", 1.50, 0),
            new Product("Milky Way", 1.50, 0),
            new Product("Twix", 1.50, 0),
            new Product("3 Musketeers", 1.50, 0),
            new Product("Water", 1.25, 0),
            new Product("Gatorade", 1.25, 0),
            new Product("", 1.25, 0),
            new Product("", 1.25, 0),
            new Product("", 1, 0),
            new Product("", 1, 0),
            new Product("", 1, 0),
            new Product("", 1, 0)
        }; // should we use switch cases for accessing elements vs. converting A1, etc to indexes??;
        coins = new CoinBox();
            currentCoins = new CoinBox();
        }

        public void restockProducts()
        {
            foreach (Product product in products)
            {
                product.Quantity = 3;
            }
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

        public string buyProduct(int element)
        {
            if (products[element].Quantity == 0) return string.Format("Out of Product {0}", products[element].getDescription());
            else
            {
                double balance = currentCoins.getValue();
                double price = products[element].getPrice();
                if (price <= balance)
                {
                    double payment = balance - price;
                    products[element].Quantity--;
                    coins.addCoins(currentCoins);
                    currentCoins.removeAllCoins();
                    return string.Format("Purchased {0}", products[element].getDescription());
                }
                else
                {
                    return string.Format("Insufficient money, {0} inserted", balance.ToString("C"));
                }
            }
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
