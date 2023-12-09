using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    class VendingMachine
    {
        private const decimal PRICE = 1.25M; // Price of everything except the bottom row of items
        private const decimal GUMPRICE = 0.50M; // Price of the bottom row of items
        public static List<Product> products;
        private decimal amountTakenFromChangeBox = 0;
        public CoinBox revenueBox = new CoinBox();
        public decimal Inserted { get; set; }
        public static System.Windows.Forms.PictureBox falling;
        public static System.Windows.Forms.PictureBox vendingMachineBot;

        /// <summary>
        /// Constructor that creates a list of all the products and their info
        /// </summary>
        /// <param name="productNames"> an array of strings that contains the names of all the products </param>
        /// <param name="boxes"> an array of PictureBoxes that contains two PictureBoxes for all the products
        /// (falling image for product 1, back image for product 1, falling for product 2, normal for product 2, etc.)</param>
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
                if (i > 11) products.Add(new Product(productNames[i], GUMPRICE, 0, pictureBoxes));
                else products.Add(new Product(productNames[i], PRICE, 0, pictureBoxes));
            }
        }

        /// <summary>
        /// Resets all of the quantities to 3 and makes all product images visible
        /// </summary>
        public void RestockProducts()
        {
            foreach (Product product in products)
            {
                product.Quantity = 3;
                product.Falling.Visible = true;
                product.Display.Visible = true;
            }
        }

        /// <summary>
        /// Creates a replicated list of the products for the purpose of referencing the list in a different class
        /// </summary>
        /// <returns> the replicated list of products </returns>
        public List<Product> GetProductTypes()
        {
            List<Product> types = new List<Product>();
            for (int i = 0; i < products.Count; i++)
            {
                Product entry = products[i];
                types.Add(entry);
            }
            return types;
        }

        /// <summary>
        /// Used to add a product to the products list
        /// </summary>
        /// <param name="p"> the product to add </param>
        public void AddProduct(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Used to add a coin to the revenueBox
        /// </summary>
        /// <param name="c"> the coin to add to the revenueBox </param>
        public void AddCoin(Coin c)
        {
            revenueBox.AddCoin(c);
            Inserted += c.GetValue();
        }

        /// <summary>
        /// If the user tries to buy a product, this makes sure there is enough quantity and money inserted to do so.
        /// </summary>
        /// <param name="element"> the index of the product the user wants to buy </param>
        /// <param name="vendingMachineBottom"> the picture of the bottom part of the vending machine </param>
        /// <param name="timer"> the timer that will make the picture fall </param>
        /// <returns> the string that will be displayed to the user, telling whether item was successfully bought or not </returns>
        public string BuyProduct(int element, System.Windows.Forms.PictureBox vendingMachineBottom, System.Windows.Forms.Timer timer)
        {
            if (element == 99) return "Not a valid input";
            if (products[element].Quantity == 0) return "Cannot Purchase: Out of Product";
            else
            {

                decimal price = products[element].GetPrice();
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

        /// <summary>
        /// Removes money from the revenueBox
        /// </summary>
        /// <param name="mode"> what mode you are looking to remove the money in. Either "revenue", or anything else </param>
        /// <returns> the amount of money removed </returns>
        public decimal RemoveMoney(string mode) 
        {
            if (mode == "revenue")
            {
                Console.WriteLine(amountTakenFromChangeBox);
                decimal r = revenueBox.GetValue() - amountTakenFromChangeBox;
                amountTakenFromChangeBox = 0;
                revenueBox.RemoveAllCoins();
                return r;
            }
            else
            {
                decimal r = Inserted;
                Inserted = revenueBox.RemovePartial(Inserted); // removes the amount possible and returns what must be taken from ChangeBox
                if (Inserted > 0) amountTakenFromChangeBox = CoinBox.RemoveRest(Inserted); // Taken from change box.
                Inserted = 0;
                return r;
            }
        }

        /// <summary>
        /// This gets the amount of money in the made in the vending machine (doesn't include anything the user currently has inserted)
        /// </summary>
        /// <returns> the amount of money made </returns>
        public decimal GetMoney()
        {
            return revenueBox.GetValue() - amountTakenFromChangeBox;
        }

        /// <summary>
        /// Makes the price of the item in the VM into a string in money format
        /// </summary>
        /// <param name="index"> the index of the product in the list </param>
        /// <returns> the string of the price in money format </returns>
        public string PriceToString(int index)
        {
            return products[index].GetPrice().ToString("C");
        }
    }
}
