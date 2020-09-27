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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<int>> listOfListNumber = new List<List<int>>();
            List<int> listNumberName = new List<int>();
            if (this.checkBox1.Checked)
            {

                    string name = IOHelper.strValid(textBox1.Text);
                    for(int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                        index = Program.g_dataBase.searchToName(name, index);
                        if (index == -1) break;
                        listNumberName.Add(index);
                    }

                listOfListNumber.Add(listNumberName);
            }

            List<int> listNumberPhone = new List<int>();
            if (this.checkBox2.Checked)
            {

                string phone = IOHelper.phoneValid(textBox2.Text);
                for (int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                    index = Program.g_dataBase.searchToPhone(phone, index);
                    if (index == -1) break;
                    listNumberPhone.Add(index);
                }

                listOfListNumber.Add(listNumberPhone);
            }

            List<int> listNumberPrice = new List<int>();
            if (this.checkBox3.Checked)
            {

                int priceMin = IOHelper.priceValid(textBox3.Text);
                int priceMax = IOHelper.priceValid(textBox4.Text);
                for (int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                    index = Program.g_dataBase.searchToPrice(priceMin, priceMax, index);
                    if (index == -1) break;
                    listNumberPrice.Add(index);
                }

                listOfListNumber.Add(listNumberPrice);
            }

            List<int> listNumberType = new List<int>();
            if (this.checkBox4.Checked)
            {

                string type = IOHelper.strValid(textBox5.Text);
                for (int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                    index =  Program.g_dataBase.searchToPropertyType(type, index);
                    if (index == -1) break;
                    listNumberType.Add(index);
                }

                listOfListNumber.Add(listNumberType);
            }

            List<int> listNumberData = new List<int>();
            if (this.checkBox5.Checked)
            {

                DateTime dataStart = IOHelper.dataValid(textBox6.Text);
                DateTime dataEnd = IOHelper.dataValid(textBox7.Text);
                for (int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                    index = Program.g_dataBase.searchToPropertyDate(dataStart, dataEnd, index);
                    if (index == -1) break;
                    listNumberData.Add(index);
                }

                listOfListNumber.Add(listNumberData);
            }

            List<int> listNumberAdrr = new List<int>();
            if (this.checkBox5.Checked)
            {

                string adrr = IOHelper.strValid(textBox8.Text);
                for (int index = 0; index < Program.g_dataBase.dataBaseSize(); index++)
                {
                    index = Program.g_dataBase.searchToAdress(adrr, index);
                    if (index == -1) break;
                    listNumberAdrr.Add(index);
                }

                listOfListNumber.Add(listNumberAdrr);
            }

            if (listOfListNumber.Count != 0)
            {
                List<int> listNumber = listOfListNumber[0];
                for (int i = 1; i < listOfListNumber.Count; i++)
                {
                    listNumber = listNumber.Intersect(listOfListNumber[1]).ToList();
                }

                if (Program.g_form.dataGridView3 is null)
                {
                    Program.g_form.dataGridView3 = new DataGridView();
                }
                while (Program.g_form.dataGridView3.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in Program.g_form.dataGridView3.Rows)
                    {
                        Program.g_form.dataGridView3.Rows.Remove(row);
                    }
                }
                for(int i = 0; i < listNumber.Count; i++)
                {
                    TransactionData recoed = Program.g_dataBase.get(listNumber.ElementAt(i));
                    String[] row = { recoed.m_name, recoed.m_phone,
                                Convert.ToString(recoed.m_price), recoed.m_adress,
                                recoed.m_propertyType , Convert.ToString(recoed.m_propertyDate)};
                    Program.g_form.dataGridView3.Rows.Add(row);
                    Program.g_form.dataGridView3.AllowUserToAddRows = false;
                }
            }

            this.Hide();
            Program.g_form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = !this.checkBox1.Checked;
            this.textBox2.ReadOnly = !this.checkBox2.Checked;
            this.textBox3.ReadOnly = !this.checkBox3.Checked;
            this.textBox4.ReadOnly = !this.checkBox3.Checked;
            this.textBox5.ReadOnly = !this.checkBox4.Checked;
            this.textBox6.ReadOnly = !this.checkBox5.Checked;
            this.textBox7.ReadOnly = !this.checkBox5.Checked;
            this.textBox8.ReadOnly = !this.checkBox6.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = !this.checkBox1.Checked;
            this.textBox2.ReadOnly = !this.checkBox2.Checked;
            this.textBox3.ReadOnly = !this.checkBox3.Checked;
            this.textBox4.ReadOnly = !this.checkBox3.Checked;
            this.textBox5.ReadOnly = !this.checkBox4.Checked;
            this.textBox6.ReadOnly = !this.checkBox5.Checked;
            this.textBox7.ReadOnly = !this.checkBox5.Checked;
            this.textBox8.ReadOnly = !this.checkBox6.Checked;
        }
    }
}
