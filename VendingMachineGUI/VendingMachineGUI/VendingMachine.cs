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
        public CoinBox revenueBox = new CoinBox();
        public decimal Inserted { get; set; }

        public VendingMachine(string[] productNames, System.Windows.Forms.PictureBox[] boxes)
        {
            Inserted = 0;
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

        public void addCoin(Coin c)
        {
            revenueBox.addCoin(c);
            Inserted += c.getValue();
        }

        public string buyProduct(int element)
        {
            if (element == 99) return "Not a valid input";
            if (products[element].Quantity == 0) return "Out of Product";
            else
            {
                decimal price = products[element].getPrice();
                if (price <= Inserted)
                {
                    Inserted -= price;
                    products[element].Quantity--;
                    return "Thank you for your Purchase";
                }
                else
                {
                    return "Insufficient money";
                }
            }
        }

        public bool productsAvailable()
        {
            return products.Count > 0;
        }

        public decimal removeMoney(string mode) // decimal has more precision than double (double doesn't calculate right)
        {
            if (mode == "revenue")
            {
                decimal r = revenueBox.getValue();
                revenueBox.removeAllCoins();
                return r;
            }
            else
            {
                decimal r = Inserted;
                Inserted = revenueBox.removePartial(Inserted);
                if (Inserted > 4) return Inserted; // C# has difficulty with precision. 1.5 - 0.5 = 1.0000001 somehow
                else return r;
            }
        }

    }
}
