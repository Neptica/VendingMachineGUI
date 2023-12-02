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
        private const bool DEVELOPERMODE = true;
        private string[] userinput = {" ", " "};
        private string pwAttempt = ""; 
        private string password = "A1D4B2C3";
        private bool privileged = false;
        private bool firstTime = true;

        public Form1()
        {
            InitializeComponent();
        }

        private VendingMachine machine;

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
            userinput[0] = " ";
            userinput[1] = " ";
            pwAttempt = "";
            DisplayUserInput();
        }
        private void DisplayUserInput()
        {
            if (DEVELOPERMODE) textBox2.Text = pwAttempt; // this is displayed for developer convenience
            textBox1.Text = string.Join("", userinput);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            adjustDisplay("A");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            adjustDisplay("B");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adjustDisplay("C");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adjustDisplay("D");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adjustDisplay(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            adjustDisplay(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adjustDisplay(3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            adjustDisplay(4);
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
                if (firstTime)
                {
                    PictureBox[] pictureArray = { ranchDoritosFalling, ranchDoritos }; // Add all the pictures in the right order.
                    machine = new VendingMachine(pictureArray);
                }
                machine.restockProducts();
                textBox2.Text = "Restocked"; // functionality not added yet
            }
            else
            {
                adjustDisplay();
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
                string input = textBox1.Text;
                if (input.Length == 2) 
                {
                    int index = 0;
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
                    }
                    textBox2.Text = machine.buyProduct(index);
                }
                else
                {
                    textBox2.Text = string.Format("Not a valid item {0}", textBox1.Text.Length);
                }
            }
        }
    }
}
