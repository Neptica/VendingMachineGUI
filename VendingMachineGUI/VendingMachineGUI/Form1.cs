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
        private Product[] products = new Product[16]; // should we use switch cases for accessing elements vs. converting A1, etc to indexes??

        public Form1()
        {
            InitializeComponent();
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
                switch (textBox1.Text)
                {
                    case "A1":
                        // code to purchase the first element
                        textBox2.Text = "Thank you for buying Cool Ranch Doritos!";
                        break;
                    case "A2":
                        // code to purchase the second element, etc
                        textBox2.Text = "Thank you for buying Doritos!";
                        break;
                    default:
                        textBox2.Text = "Please input the Item you would like to purchase";
                        break;
                }
            }
        }
    }
}
