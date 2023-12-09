using System;
using System.Drawing;
using System.Windows.Forms;

namespace VendingMachineGUI
{
    public partial class Form1 : Form
    {
        private VendingMachine machine;
        private const bool DEVELOPERMODE = false;
        private string[] userInput = { "", "" }; // This is used to store the two different characters of the code for the product
        private string pwAttempt = ""; // This is used to access the privelaged mode; it stores the password; # to reset password attempt
        private readonly string password = "A1D4B2C3"; // Password to match to enter privelage mode
        private bool privileged = false;
        private readonly string[] productNames = {
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
        }; // Stores all 16 product names for when they are passed to become objects

        /// <summary>
        /// Contructor for the form; initializes picture references in a list and creates a vending machine
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            PictureBox[] pictureArray = {
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
                mentos,
            }; // Holds all of the pictures we need for the animations; a falling one and a still one; will be passed to create product objects
            machine = new VendingMachine(productNames, pictureArray);
            textBox2.Text = "Welcome to Vending Machine"; 
            textBox3.Text = machine.Inserted.ToString("C");
        }

        /// <summary>
        /// Receives an input from the keypad and adds it to psAttempt and checks to see if it can display data for a product on screen
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that was pushed (could be from letter A-D and number 1-4 buttons) </param>
        /// <param name="e"> contains the event data </param>
        private void Keypad(object sender, EventArgs e)
        {
            Button sentBy = (Button)sender;
            string key = sentBy.Text;
            int result;
            if (int.TryParse(key, out result)) AdjustDisplay(result);
            else AdjustDisplay(key);
            if (!privileged) // If the person typing is not the owner of the machine (AKA VM is not in privelage mode)
            {
                if (userInput[0] != "" && userInput[1] != "") // If there is a letter and a number typed in
                {
                    int index = 99;
                    index = GetProductIndex(userInput[0] + userInput[1]);
                    if (index != 99)
                    { // This will display the product price and the product description for the user
                        if (machine.GetProductTypes()[index].Quantity == 0) textBox2.Text = "Out of Stock";
                        else
                        {
                            textBox2.Text = machine.GetProductTypes()[index].GetDescription();
                            textBox3.Text = machine.PriceToString(index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Used for clearing letters/numbers and login attempts, or in privelage mode it is used to view or take out money
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object Hashtag </param>
        /// <param name="e"> contains the event data </param>
        private void Hashtag(object sender, EventArgs e)
        {
            if (pwAttempt == password) // If the password typed in is correct
            {
                privileged = !privileged; // this flips the mode (exits admin mode the using the same password as alternative to Logout button)
                if (privileged) // If it enters into privelage mode
                {
                    textBox1.Text = "";
                    textBox2.Text = "Priviliged Mode:\r\nA# - View $\r\nB# - Take $";
                    button10.Text = "Restock";
                    button11.Text = "Logout";
                }
                else // If it is exiting privelage mode using the password hashtag method
                {
                    string removed = machine.RemoveMoney("revenue").ToString("C");
                    if (removed == "$0.00") textBox2.Text = "Nothing to Remove";
                    else textBox2.Text = string.Format("Profited {0} and Entering Customer Mode", removed);
                    machine.Inserted = 0; 
                    textBox3.Text = "0.00"; // the owner cannot use it at the same time as a user, so if there is money in the machine, the user forgot it, and it will just be taken out
                    button10.Text = "Eject";
                    button11.Text = "Purchase";
                }
                pwAttempt = "";
                userInput[0] = " ";
                userInput[1] = " ";
            }
            else if (privileged) // This is for the owner in privelage mode
            {
                if (pwAttempt == "A") // If they type A followed by a #, it views the total money in the machine
                {
                    textBox2.Text = String.Format("Total amount of money in the machine: {0}", machine.GetMoney().ToString("C"));
                    pwAttempt = "";
                }
                else if (pwAttempt == "B") // If they type B followed by a #, it takes out the total money in the machine
                {
                    string removed = machine.RemoveMoney("revenue").ToString("C");
                    if (removed == "$0.00") textBox2.Text = "Nothing to Remove";
                    else textBox2.Text = "Total amount of money removed from the machine" + removed;
                    machine.Inserted = 0; 
                    textBox3.Text = "0.00"; // the owner cannot use it at the same time as a user, so if there is money in the machine, the user forgot it, and it will just be taken out
                    pwAttempt = "";
                }
            } 
            else
            {
                AdjustDisplay();
            }
        }

        /// <summary>
        /// If in privelaged mode, it will restock all of the products. If in normal mode, it will give the user their change back.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object button that either reads Eject or Restock, depending on mode </param>
        /// <param name="e"> contains the event data </param>
        private void EjectRestock(object sender, EventArgs e)
        {
            if (privileged) // In privelaged mode
            {
                machine.RestockProducts();
                textBox2.Text = "Restocked";
            }
            else // In normal mode
            {
                AdjustDisplay();
                string removed = machine.RemoveMoney("current").ToString("C");
                if (removed == "$0.00") textBox2.Text = "Nothing to Eject";
                else textBox2.Text = string.Format("Removed {0}", removed);
                textBox3.Text = machine.Inserted.ToString("C");
            }
        }

        /// <summary>
        /// If in privelaged mode, this turns it back to normal mode. If in normal mode, this purchases the product if there is enough quantity and user-inserted money
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object button that either reads Purchase or Logout, depending on mode </param>
        /// <param name="e"> contains the event data </param>
        private void PurchaseLogout(object sender, EventArgs e)
        {
            if (privileged)
            {
                textBox2.Text = "Customer Mode";
                button10.Text = "Eject";
                button11.Text = "Purchase";
                privileged = !privileged;
                textBox2.Text = "Logged out";
                textBox1.Text = "";
                userInput[0] = "";
                userInput[1] = "";
            }
            else
            {
                string input = textBox1.Text.Replace(" ", ""); // removes whitespaces
                if (input.Length == 2)
                {
                    int index = GetProductIndex(input);
                    textBox1.Text = "";
                    userInput[0] = "";
                    userInput[1] = "";
                    textBox2.Text = machine.BuyProduct(index, vendingMachineBottom, timer1);
                    textBox3.Text = machine.Inserted.ToString("C");
                }
            }
        }

        /// <summary>
        /// This adds money to the machine. Amount varies based on which button was pushed.
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that was pushed (could 5 cent up to 5 dollar buttons) </param>
        /// <param name="e"> contains the event data </param>
        private void Money(object sender, EventArgs e)
        {
            Button sentBy = (Button)sender;
            string amount = sentBy.Text;
            switch (amount)
            {
                case "5¢":
                    machine.AddCoin(Coin.NICKEL);
                    textBox2.Text = "You just inserted $0.05\r\n\r\n\r\nTotal:";
                    break;
                case "10¢":
                    machine.AddCoin(Coin.DIME);
                    textBox2.Text = "You just inserted $0.10\r\n\r\n\r\nTotal:";
                    break;
                case "25¢":
                    machine.AddCoin(Coin.QUARTER);
                    textBox2.Text = "You just inserted $0.25\r\n\r\n\r\nTotal:";
                    break;
                case "50¢":
                    machine.AddCoin(Coin.HALFDOLLAR);
                    textBox2.Text = "You just inserted $0.50\r\n\r\n\r\nTotal:";
                    break;
                case "$1":
                    machine.AddCoin(Coin.DOLLAR); 
                    textBox2.Text = "You just inserted $1.00\r\n\r\n\r\nTotal:";
                    break;
                case "$5":
                    machine.AddCoin(Coin.FIVER);
                    textBox2.Text = "You just inserted $5.00\r\n\r\n\r\nTotal:";
                    break;
                default:
                    textBox2.Text = "something went wrong, in Money method";
                    break;
            }
            textBox3.Text = machine.Inserted.ToString("C");
        }

        /// <summary>
        /// This is an overloaded method. This one takes an int as input, and then it adds it to pwAttempt and changes the number of product code to it
        /// </summary>
        /// <param name="input"> an int that the user pushes on the keypad </param>
        private void AdjustDisplay(int input)
        {
            pwAttempt += input;
            userInput[1] = Convert.ToString(input);
            DisplayUserInput();
        }

        /// <summary>
        /// This is an overloaded method. This one takes a string as input, and then it adds it to pwAttempt and changes the letter of product code to it
        /// </summary>
        /// <param name="input"> a letter that the user pushes on the keypad </param>
        private void AdjustDisplay(string input)
        {
            pwAttempt += input;
            userInput[0] = input;
            DisplayUserInput();
        }

        /// <summary>
        /// This is an overloaded method. This resets all of the pwAttempt and userInput data
        /// </summary>
        private void AdjustDisplay()
        {
            userInput[0] = "";
            userInput[1] = "";
            pwAttempt = "";
            DisplayUserInput();
        }

        /// <summary>
        /// This is only for developer mode purposes. It displays what the pwAttempt would be
        /// </summary>
        private void DisplayUserInput()
        {
            if (DEVELOPERMODE) textBox2.Text = pwAttempt; // this is displayed for developer convenience
            textBox1.Text = string.Join("", userInput);
        }

        /// <summary>
        /// This sets the index for the correct picture to fall in the VM, and it starts the timer, which makes the picture fall.
        /// </summary>
        /// <param name="timer"> the timer that is being started </param>
        /// <param name="element"> the index of the picture that is going to fall </param>
        public static void MakePictureFall(System.Windows.Forms.Timer timer, int element)
        {
            subscript = element;
            timer.Start();
        }

        private int velocity = 0;
        private int ticks = 0;
        private int tempTop;
        private static int subscript;

        /// <summary>
        /// Makes the image in the vending machine appear like it is falling
        /// </summary>
        /// <param name="sender"> contains a reference to the control/object that raised the event </param>
        /// <param name="e"> contains the event data </param>
        public async void timer1_Tick(object sender, EventArgs e)
        {
            if (ticks == 0) // Only runs this on the very first tick (before anything has moved)
            {
                tempTop = VendingMachine.falling.Top; // for resetting it to starting position
                button11.BackColor = Color.DarkGray; // Allows user to know the purchase button isn't active while product is falling
                button11.Enabled = false; // Makes purchase button inactive while product is falling
            }
            ++ticks;
            const int ACCELERATION = 1;
            velocity += ACCELERATION;
            VendingMachine.falling.Top += (int)velocity;
            if (VendingMachine.falling.Top >= VendingMachine.vendingMachineBot.Top) // If the product has fully fallen
            {
                button11.BackColor = Color.FromArgb(224, 224, 224); // reset purchase button 
                button11.Enabled = true; // reset purchase button
                velocity = 0;
                ticks = 0;
                VendingMachine.falling.Top = tempTop;// resets moving pic to original spot
                timer1.Stop();
                if (VendingMachine.products[subscript].Quantity == 1) // Hides back image if quantity is one
                {
                    VendingMachine.products[subscript].Display.Visible = false;
                }
                if (VendingMachine.products[subscript].Quantity == 0) // Hides front image (moving one) if quatity is 0
                {
                    VendingMachine.products[subscript].Falling.Visible = false;
                }
            }
        }

        /// <summary>
        /// Takes in code of product and deciphers where that is in the list
        /// </summary>
        /// <param name="input"> a string that is a code for a product in the VM</param>
        /// <returns> the index of that product in the list </returns>
        private static int GetProductIndex(string input)
        {
            int index = 0;
            switch (input)
            {
                case "A1":
                    index = 0;
                    break;
                case "A2":
                    index = 1;
                    break;
                case "A3":
                    index = 2;
                    break;
                case "A4":
                    index = 3;
                    break;
                case "B1":
                    index = 4;
                    break;
                case "B2":
                    index = 5;
                    break;
                case "B3":
                    index = 6;
                    break;
                case "B4":
                    index = 7;
                    break;
                case "C1":
                    index = 8;
                    break;
                case "C2":
                    index = 9;
                    break;
                case "C3":
                    index = 10;
                    break;
                case "C4":
                    index = 11;
                    break;
                case "D1":
                    index = 12;
                    break;
                case "D2":
                    index = 13;
                    break;
                case "D3":
                    index = 14;
                    break;
                case "D4":
                    index = 15;
                    break;
                default:
                    index = 99;
                    break;
            }
            return index;
        }
    }
}
