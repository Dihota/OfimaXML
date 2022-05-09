using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prueba;

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
            Class1 C1 = new Class1();
            string dato =  C1.xmlread(@"C:\Users\57320\Documents\XML\SAPHETY_ad08001323020252200000425.xml");



            string[] array = dato.Split('\u002C');

            textBox1.Text = array[0];
            textBox2.Text = array[1];
            textBox3.Text = array[2];

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
