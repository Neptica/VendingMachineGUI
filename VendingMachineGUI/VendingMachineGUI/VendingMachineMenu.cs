using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VendingMachineGUI
{
    class VendingMachineMenu
    {
        // private string input;
        private static Coin[] COINS = {
    Coin.NICKEL, Coin.DIME, Coin.QUARTER, Coin.DOLLAR, Coin.FIVER
  };

        public VendingMachineMenu()
        {
            //input = ReadLine();
        }

        //public void run(VendingMachine machine)
        //{
        //    bool more = true;

        //    while (more)
        //    {
        //        WriteLine("S)how products A)dd B)uy product I)nsert R)emove coins Q)uit: ");
        //        string command = ReadLine().ToUpper();
        //        char com = command[0];

        //        switch (com)
        //        {
        //            case 'S':
        //                foreach (Product p in machine.getProductTypes()) // doesn't return a product description.
        //                {
        //                    WriteLine(p.toString());
        //                }
        //                break;
        //            case 'A':
        //                WriteLine("Description: ");
        //                string description = ReadLine();
        //                WriteLine("Price: ");
        //                double price = Convert.ToDouble(ReadLine());
        //                WriteLine("Quantity: ");
        //                int quantity = Convert.ToInt32(ReadLine());
        //                // ReadLine();
        //                machine.addProduct(new Product(description, price, quantity));
        //                break;
        //            case 'I':
        //                Coin chosen = pickCoin(); // doesn't show proper Coin names
        //                WriteLine("Current $ " + machine.addCoin(chosen));
        //                break;
        //            case 'B':
        //                Product pro = pickProduct(machine.getProductTypes());
        //                String result = machine.buyProduct(pro);
        //                if (result == "OK")
        //                {
        //                    WriteLine("Purchased: " + pro.toString());
        //                }
        //                else
        //                {
        //                    WriteLine("Sorry: " + result);
        //                }
        //                break;
        //            case 'R':
        //                double totalInCoinBox = machine.removeMoney();
        //                String totalString = totalInCoinBox.ToString("C");
        //                WriteLine("Removed: " + totalString);
        //                break;
        //            case 'Q':
        //                more = false;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private Coin pickCoin()
        //{
        //    while (true)
        //    {
        //        char c = 'A';
        //        foreach (Coin choice in COINS)
        //        {
        //            WriteLine(c + ") " + choice.getName());
        //            c++;
        //        }
        //        string input = ReadLine();
        //        string inputU = input.ToUpper();
        //        int n = inputU[0] - 'A';
        //        if (0 <= n && n < COINS.Length)
        //        {
        //            return COINS[n];
        //        }
        //    }
        //}


        //// Pick a product from all products
        //private Product pickProduct(List<Product> allProducts)
        //{
        //    while (true)
        //    {
        //        char c = 'A';
        //        foreach (Product choice in allProducts)
        //        {
        //            WriteLine(c + ") " + choice.toString());
        //            c++;
        //        }
        //        string input = ReadLine();
        //        string inputUpper = input.ToUpper();
        //        int n = inputUpper[0] - 'A';
        //        if (0 <= n && n < allProducts.Count)
        //        {
        //            return allProducts[n];
        //        }
        //    }
        //}

        //public static void Main()
        //{
        //    new VendingMachineMenu()
        //       .run(new VendingMachine());
        //}
    }
}
