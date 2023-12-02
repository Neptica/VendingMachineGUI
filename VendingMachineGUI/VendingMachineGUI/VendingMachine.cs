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
        
        public VendingMachine(string[] productNames, System.Windows.Forms.PictureBox[] boxes) // Fix this
        {
            products = new List<Product>();
            int elementStart = 0;
            System.Windows.Forms.PictureBox[] pictureBoxes = new System.Windows.Forms.PictureBox[2];
            for (int i = 0; i < 16; i++)
            {
                pictureBoxes[0] = boxes[elementStart];
                pictureBoxes[1] = boxes[elementStart + 1];
                elementStart += 2;
                products.Add(new Product(productNames[i], 1.25, 0, pictureBoxes));
            }
            coins = new CoinBox();
            currentCoins = new CoinBox();
        }


        public void restockProducts()
        {
            foreach (Product product in products)
            {
                product.Quantity = 3;
                product.Falling.Visible = true;
                product.Display.Visible = true;
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
            if (element == 99) return ("Not a valid item");
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
