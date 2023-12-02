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

    public VendingMachine(System.Windows.Forms.PictureBox[] boxes) // Fix this
    {
      products = new List<Product>();
      int elementStart = 0;
      System.Windows.Forms.PictureBox[] pictureBoxes = { boxes[elementStart], boxes[elementStart + 1] };
      Product temp = new Product("Cool Ranch Doritos", 1.25, 0, pictureBoxes);
      products.Add(temp);
      //for (int i = 0; i < 16; i++)
      //{
      //    System.Windows.Forms.PictureBox[] pictureBoxes = { boxes[elementStart], boxes[elementStart + 1] };
      //    products.Add(new Product("Cool Ranch Doritos", 1.25, 0, pictureBoxes));
      //}
      //new Product("Cool Ranch Doritos", 1.25, 0, { boxes[0], boxes[1] }),
      //new Product("Doritos", 1.25, 0, ref { boxes[2], boxes[3]}),
      //new Product("Cheetos", 1.25, 0, ref { boxes[4], boxes[5] }),
      //new Product("Funyuns", 1.25, 0, ref { boxes[6], boxes[7] }),
      //new Product("Snickers", 1.50, 0, ref boxes[8], ref boxes[9]),
      //new Product("Milky Way", 1.50, 0, ref boxes[10], ref boxes[11]),
      //new Product("Twix", 1.50, 0, ref boxes[12], ref boxes[13]),
      //new Product("3 Musketeers", 1.50, 0, ref boxes[14], ref boxes[15]),
      //new Product("Water", 1.25, 0, ref boxes[16], ref boxes[17]),
      //new Product("Gatorade", 1.25, 0, ref boxes[18], ref boxes[19]),
      //new Product("", 1.25, 0, ref boxes[20], ref boxes[21]),
      //new Product("", 1.25, 0, ref boxes[22], ref boxes[23]),
      //new Product("", 1, 0, ref boxes[24], ref boxes[25]),
      //new Product("", 1, 0, ref boxes[26], ref boxes[27]),
      //new Product("", 1, 0, ref boxes[28], ref boxes[29]),
      //new Product("", 1, 0, ref boxes[30], ref boxes[31])
      coins = new CoinBox();
      currentCoins = new CoinBox();
    } // should we use switch cases for accessing elements vs. converting A1, etc to indexes??;


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
