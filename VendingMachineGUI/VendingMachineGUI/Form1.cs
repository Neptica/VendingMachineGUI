using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VendingMachineGUI
{
    public partial class Form1 : Form
    {
        private VendingMachine machine;
        private const bool DEVELOPERMODE = false;
        private string[] userinput = { "", "" };
        private string pwAttempt = "";
        private string password = "A1D4B2C3";
        private bool privileged = false;
        private string[] productNames = {
            "Cool Ranch Doritos",
            "Doritos",
            "Cheetos",
            "Funyuns",
            "Snickers",
            "Milkyway",
            "Twix",
            "3 Musketeers",
            "Water",
            "Red Gatorade",
            "Orange Gatorade",
            "Blue Gatorade",
            "Spearmint Gum",
            "Peppermint Gum",
            "Watermelon Gum",
            "Mentos"
        };

        public Form1()
        {
            InitializeComponent();
            PictureBox[] pictureArray = { // it won't let me initialize this array outside a method.
                ranchDoritosFalling,
                ranchDoritos,
                nachoDoritosFalling,
                nachoDoritos,
                cheetosFalling,
                cheetos,
                funyunsFalling,
                funyons,
                snickersFalling,
                snickers,
                milkywayFalling,
                milkyway,
                twixFalling,
                twix,
                threeMusketFalling,
                threeMusket,
                waterFalling,
                water,
                redGatoradeFalling,
                redGatorade,
                orangeGatoradeFalling,
                orangeGatorade,
                blueGatoradeFalling,
                blueGatorade,
                spearmintGumFalling,
                spearmintGum,
                peppermintGumFalling,
                peppermintGum,
                watermelonGumFalling,
                watermelonGum,
                mentosFalling,
                mentos
            };
            machine = new VendingMachine(productNames, pictureArray);
            textBox2.Text = "Welcome to Vending Machine";
            textBox3.Text = machine.Inserted.ToString("C");
        }



        private void Keypad(object sender, EventArgs e)
        {
            Button sentBy = (Button)sender;
            string key = sentBy.Text;
            int result;
            if (int.TryParse(key, out result)) adjustDisplay(result);
            else adjustDisplay(key);
        }

        private void button9_Click(object sender, EventArgs e) // Hashtag (Clear and Login Attempt)
        {
            if (pwAttempt == password)
            {
                privileged = !privileged; // this flips the mode (exits admin mode the using the same password)
                if (privileged)
                {
                    textBox2.Text = "Priviliged Mode";
                    button10.Text = "Restock";
                    button11.Text = "Logout";
                }
                else
                {
                    textBox2.Text = "Customer Mode";
                    button10.Text = "Eject";
                    button11.Text = "Purchase";
                }
                pwAttempt = "";
                userinput[0] = " ";
                userinput[1] = " ";
            }
            else
            {
                adjustDisplay();
            }
        }

        private void button10_Click(object sender, EventArgs e) // Eject
        {
            if (privileged)
            {
                machine.restockProducts();
                textBox2.Text = "Restocked"; // functionality not added yet
            }
            else
            {
                adjustDisplay();
                textBox2.Text = string.Format("Removed {0}", machine.removeMoney("current").ToString("C"));
                textBox3.Text = machine.Inserted.ToString("C");
            }
        }

        private void button11_Click(object sender, EventArgs e) // Purchase
        {
            if (privileged)
            {
                textBox2.Text = "Customer Mode";
                button10.Text = "Eject";
                button11.Text = "Purchase";
                privileged = !privileged;
                textBox2.Text = "Logged out"; // functionality not added yet
            }
            else
            {
                string input = textBox1.Text.Replace(" ", ""); // removes whitespaces DO NOT REMOVE
                if (input.Length == 2)
                {
                    int index = 99;
                    switch (input)
                    {
                        case "A1":
                            // code to purchase the first element
                            index = 0;
                            break;
                        case "A2":
                            // code to purchase the second element, etc
                            index = 1;
                            break;
                        case "A3":
                            // code to purchase the first element
                            index = 2;
                            break;
                        case "A4":
                            // code to purchase the second element, etc
                            index = 3;
                            break;
                        case "B1":
                            // code to purchase the first element
                            index = 4;
                            break;
                        case "B2":
                            // code to purchase the second element, etc
                            index = 5;
                            break;
                        case "B3":
                            // code to purchase the first element
                            index = 6;
                            break;
                        case "B4":
                            // code to purchase the second element, etc
                            index = 7;
                            break;
                        case "C1":
                            // code to purchase the first element
                            index = 8;
                            break;
                        case "C2":
                            // code to purchase the second element, etc
                            index = 9;
                            break;
                        case "C3":
                            // code to purchase the first element
                            index = 10;
                            break;
                        case "C4":
                            // code to purchase the second element, etc
                            index = 11;
                            break;
                        case "D1":
                            // code to purchase the first element
                            index = 12;
                            break;
                        case "D2":
                            // code to purchase the second element, etc
                            index = 13;
                            break;
                        case "D3":
                            // code to purchase the first element
                            index = 14;
                            break;
                        case "D4":
                            // code to purchase the second element, etc
                            index = 15;
                            break;
                        default:
                            textBox2.Text = "not possible";
                            break;
                    }
                    textBox2.Text = machine.buyProduct(index);
                    textBox3.Text = machine.Inserted.ToString("C");
                }
            }
        }

        private void Money(object sender, EventArgs e)
        {
            Button sentBy = (Button)sender;
            string amount = sentBy.Text;
            switch (amount)
            {
                case "5¢":
                    machine.addCoin(Coin.NICKEL);
                    break;
                case "10¢":
                    machine.addCoin(Coin.DIME);
                    break;
                case "25¢":
                    machine.addCoin(Coin.NICKEL);
                    break;
                case "50¢":
                    machine.addCoin(Coin.HALFDOLLAR);
                    break;
                case "$1":
                    machine.addCoin(Coin.DOLLAR);
                    break;
                case "$5":
                    machine.addCoin(Coin.FIVER);
                    break;
                default:
                    textBox2.Text = "something went wrong, in Money method";
                    break;
            }
            textBox3.Text = machine.Inserted.ToString("C");
        }
        private void adjustDisplay(int input)
        {
            pwAttempt += input;
            userinput[1] = Convert.ToString(input);
            DisplayUserInput();
        }
        private void adjustDisplay(string input)
        {
            pwAttempt += input;
            userinput[0] = input;
            DisplayUserInput();
        }
        private void adjustDisplay()
        {
            userinput[0] = "";
            userinput[1] = "";
            pwAttempt = "";
            DisplayUserInput();
        }
        private void DisplayUserInput()
        {
            if (DEVELOPERMODE) textBox2.Text = pwAttempt; // this is displayed for developer convenience
            textBox1.Text = string.Join("", userinput);
        }
    }
}
