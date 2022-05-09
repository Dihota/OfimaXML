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
            string xml = @"C:\Users\Hogar\Documents\Diego\ofima\Desarrollo\Lector Compra\XML\" + textBox1.Text;

            LectorComprasFacturacionElectronica LecturaXML = new LectorComprasFacturacionElectronica();
            string dato =  LecturaXML.XmlEmisor(xml);

            //Se declara array para obtener los datos del emisor.
            string[] Emisor = dato.Split('\u002C');

            //Se asignan los valores del emisor a su respectivo textbox.
            label5.Text = Emisor[2];
            label6.Text = Emisor[0];
            label7.Text = Emisor[1];
            label8.Text = Emisor[4];
            label9.Text = Emisor[3];

            string dato2 = LecturaXML.XmlAdquiriente(xml);
            string[] Adqui = dato2.Split('\u002C');
            label17.Text = Adqui[0];
            label14.Text = Adqui[1];




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
