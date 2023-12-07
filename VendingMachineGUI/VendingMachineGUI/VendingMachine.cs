using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    class VendingMachine
    {
        private const decimal PRICE = 1.25M;
        public static List<Product> products;
        private decimal amountTakenFromChangeBox = 0;
        public CoinBox revenueBox = new CoinBox();
        public decimal Inserted { get; set; }
        public static System.Windows.Forms.PictureBox falling;
        public static System.Windows.Forms.PictureBox vendingMachineBot;

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
                products.Add(new Product(productNames[i], PRICE, 0, pictureBoxes));
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

        public string buyProduct(int element, System.Windows.Forms.PictureBox vendingMachineBottom, System.Windows.Forms.Timer timer)
        {
            if (element == 99) return "Not a valid input";
            if (products[element].Quantity == 0) return "Cannot Purchase: Out of Product";
            else
            {

                decimal price = products[element].getPrice();
                if (price <= Inserted)
                {
                    Inserted -= price;
                    products[element].Quantity--;
                    falling = products[element].Falling;
                    vendingMachineBot = vendingMachineBottom;
                    Form1.MakePictureFall(timer, element);
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

        public decimal removeMoney(string mode) // decimal has more precision than decimal (decimal doesn't calculate right)
        {
            if (mode == "revenue")
            {
                Console.WriteLine(amountTakenFromChangeBox);
                decimal r = revenueBox.getValue() - amountTakenFromChangeBox;
                amountTakenFromChangeBox = 0;
                revenueBox.removeAllCoins();
                return r;
            }
            else
            {
                decimal r = Inserted;
                Inserted = revenueBox.removePartial(Inserted); // removes the amount possible and returns what must be taken from ChangeBox
                if (Inserted > 0) amountTakenFromChangeBox = CoinBox.removeRest(Inserted); // Taken from change box.
                Inserted = 0;
                return r;
            }
        }

        public decimal getMoney()
        {
            return revenueBox.getValue();
        }
        public string priceToString()
        {
            return PRICE.ToString("C");
        }
    }
}
