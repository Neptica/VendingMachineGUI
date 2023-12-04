using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    public class Product
    {
        private string description;
        private decimal price;
        public System.Windows.Forms.PictureBox Falling;
        public System.Windows.Forms.PictureBox Display;
        public Product(string aDescription, decimal aPrice, int aQuantity, System.Windows.Forms.PictureBox[] pictures)
        {
            description = aDescription;
            price = aPrice;
            Quantity = aQuantity;
            Falling = pictures[0];
            Display = pictures[1];
        }

        public int Quantity { get; set; }

        public string getDescription()
        {
            return description;
        }

        public decimal getPrice()
        {
            return price;
        }

        public new bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            Product b = (Product)other;
            return description.Equals(b.description) && price == b.price;
        }

        public string toString()
        {
            string priceString = price.ToString("C");
            return description + " @ " + priceString;
        }
    }
}
