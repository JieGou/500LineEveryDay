using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string str = "\\";
            string p_strOne,
                   p_strTwo,
                   p_strThree;
            p_strOne = "Hello,\"C#\";";
            p_strTwo = "Hello," + '\u0022' + "C#" + '\u0022' + ";";
            p_strThree = "hello," + str + "C#" + str + ";";

            MessageBox.Show(p_strOne + "    " + p_strTwo + "    " + p_strThree);
        }

        private void button2_Click(object sender, EventArgs e)
        {
          new C017Form().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}