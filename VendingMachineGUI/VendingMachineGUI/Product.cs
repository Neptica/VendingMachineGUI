using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineGUI
{
    public class Product
    {
        private readonly string description;
        private readonly decimal price;
        public System.Windows.Forms.PictureBox Falling;
        public System.Windows.Forms.PictureBox Display;

        /// <summary>
        /// Constructor that creates a product 
        /// </summary>
        /// <param name="aDescription"> the name of the product (i.e. Snickers) </param>
        /// <param name="aPrice"> the price of the product </param>
        /// <param name="aQuantity"> the quantity available of the product </param>
        /// <param name="pictures"> the pictures of the product (first one in array is the falling image, second is normal image) </param>
        public Product(string aDescription, decimal aPrice, int aQuantity, System.Windows.Forms.PictureBox[] pictures)
        {
            description = aDescription;
            price = aPrice;
            Quantity = aQuantity;
            Falling = pictures[0];
            Display = pictures[1];
        }

        public int Quantity { get; set; }

        /// <summary>
        /// Gets the name of the product
        /// </summary>
        /// <returns> the name of the product (i.e. Snickers) </returns>
        public string GetDescription()
        {
            return description;
        }

        /// <summary>
        /// Gets the price of the product
        /// </summary>
        /// <returns> the price of the product </returns>
        public decimal GetPrice()
        {
            return price;
        }

        /// <summary>
        /// Checks to see if product is the same as the other based on name and price
        /// </summary>
        /// <param name="other"> the other product that is being compared to this one </param>
        /// <returns> true if other product matches description/name AND price, false otherwise </returns>
        public new bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            Product b = (Product)other;
            return description.Equals(b.description) && price == b.price;
        }
    }
}
