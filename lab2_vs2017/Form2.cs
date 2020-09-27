using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_vs2017
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(textBox1.Text);
            if(index > 0 && index <= Program.g_dataBase.dataBaseSize())
            {
                Program.g_form.swapInDataBase(index - 1);
                this.Hide();
                Program.g_form.Show();
            }
            else
            {
                //exeption incorrect value int from 1 to Program.g_dataBase.dataBaseSize()
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
