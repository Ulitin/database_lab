using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab2_vs2017
{
    public partial class DataBaseWorker : Form
    {
        public DataBaseWorker()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PleaseClose();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null)
            {
                // WRN crutch
                Program.g_dataBase = new DataBase(1024);
            }
            else
            {
                // added logic for check actual data in database and output question to user
            }
            Program.updateInfoFromDataBase();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if((!(Program.g_dataBase is null)) && Program.g_dataBase.neededSave())
            {
                //output warning
                // if (user want save choose)
                // сохранитьКакToolStripMenuItem_Click();
            }
            string path = null;
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                path = choofdlog.FileName;
            }
            if (IOHelper.isReadFile(path))
            {
                Program.g_dataBase = IOHelper.createDataBaseFromFile(path);
                Program.g_path = path;
                if(Program.g_dataBase is null)
                {
                    Program.g_dataBase = null;
                    bool bbb = Program.g_dataBase.neededSave();
                    //exeption
                    return;
                }
            }
            else
            {
                Program.g_dataBase = null;
                bool bbb = Program.g_dataBase.neededSave();
                //exeption
                return;
            }
            Program.updateInfoFromDataBase();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.g_dataBase is null)
            {
                //exeption
                return;
            }
            string path = null;
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                path = choofdlog.FileName;
            }
            if (IOHelper.isWrittenFile(path))
            {
                IOHelper.createFileFromDataBase(Program.g_dataBase, path);
                Program.g_path = Path.GetFileName(path);
            }
            else
            {
                // exeption
                return;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((!(Program.g_dataBase is null)) && IOHelper.isWrittenFile(Program.g_path))
            {
                IOHelper.createFileFromDataBase(Program.g_dataBase, Program.g_path);
            }
            else
            {
                // exeption
                return;
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if((!(Program.g_dataBase is null)) && Program.g_dataBase.neededSave())
            {
                //output message
            }
            Program.g_dataBase = null;
            Program.g_path = "";
            Program.updateInfoFromDataBase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransactionData tmp = IOHelper.createTransactionData(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);
            if (Program.g_dataBase is null || dataGridView1 is null || tmp is null)
            {
                //exeption
                return;
            }
            if (Program.g_dataBase.add(tmp))
            {
                String[] row = { tmp.m_name, tmp.m_phone,
                                    Convert.ToString(tmp.m_price), tmp.m_adress,
                                    tmp.m_propertyType , tmp.m_propertyDate.ToString("dd/MM/yyyy")};
                dataGridView1.Rows.Add(row);
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransactionData tmp = IOHelper.createTransactionData(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);
            int index = Program.g_dataBase.search(tmp);
            if (index >= 0 && index < Program.g_dataBase.dataBaseSize())
            {
                if (Program.g_dataBase.delete(tmp))
                {
                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        TransactionData iter = IOHelper.createTransactionDataFromRow(row);
                        if (iter == tmp)
                        {
                            dataGridView1.Rows.RemoveAt(row.Index);
                            break;
                        }
                    }
                }
                else
                {
                    // WRN data base dont have this record
                }
            }
            else
            {
                //exeption
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            Program.g_form.Hide();
            newForm.Show();
        }

        public void swapInDataBase(int index)
        {
            TransactionData tmp = IOHelper.createTransactionData(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);
            if (Program.g_dataBase is null || dataGridView1 is null)
            {
                //exeption
                return;
            }
            if (Program.g_dataBase.swap(index, tmp))
            {
                String[] row = { tmp.m_name, tmp.m_phone,
                                    Convert.ToString(tmp.m_price), tmp.m_adress,
                                    tmp.m_propertyType , tmp.m_propertyDate.ToString("dd/MM/yyyy")};
                dataGridView1.Rows.Remove(dataGridView1.Rows[index]);
                dataGridView1.Rows.Insert(index, row);
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TransactionData tmp = IOHelper.createTransactionData(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);
            if (Program.g_dataBase is null || dataGridView1 is null)
            {
                //exeption
                return;
            }
            int index = Program.g_dataBase.search(tmp);
            if(index == -1)
            {
                // informing
                // element not found
                return;
            }
            Program.g_form.dataGridView1.ClearSelection();
            Program.g_form.dataGridView1.CurrentCell = Program.g_form.dataGridView1.Rows[index].Cells[0];
            Program.g_form.dataGridView1.Rows[index].Selected = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TransactionData tmp = IOHelper.createTransactionData(textBox1.Text, textBox2.Text, textBox6.Text, textBox5.Text, textBox3.Text, textBox4.Text);
            if (Program.g_dataBase is null || dataGridView1 is null)
            {
                //exeption
                return;
            }
            int startSearchIndex = Program.g_form.dataGridView1.CurrentRow.Index + 1;
            int index = Program.g_dataBase.searchNext(tmp, startSearchIndex);
            if (index == -1)
            {
                // informing
                // element not found
                return;
            }
            Program.g_form.dataGridView1.ClearSelection();
            Program.g_form.dataGridView1.CurrentCell = Program.g_form.dataGridView1.Rows[index].Cells[0];
            Program.g_form.dataGridView1.Rows[index].Selected = true;
        }

        private void имяКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[0].Visible = !Program.g_form.dataGridView1.Columns[0].Visible;
        }

        private void телефонКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[1].Visible = !Program.g_form.dataGridView1.Columns[1].Visible;
        }

        private void ценаСделкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[2].Visible = !Program.g_form.dataGridView1.Columns[2].Visible;
        }

        private void адресМестонахожденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[3].Visible = !Program.g_form.dataGridView1.Columns[3].Visible;
        }

        private void видНедвижимостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[4].Visible = !Program.g_form.dataGridView1.Columns[4].Visible;
        }

        private void датаСделкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.g_form.dataGridView1.Columns[5].Visible = !Program.g_form.dataGridView1.Columns[5].Visible;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            Program.g_form.Hide();
            newForm.Show();
        }

        private void сортироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form4 newForm = new Form4();
            //Program.g_form.Hide();
            //newForm.Show();
        }

        private void поИмениКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByName();
            Program.updateInfoFromDataBase();
        }

        private void поТелефонуКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByPhone();
            Program.updateInfoFromDataBase();
        }

        private void поЦенеСделкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByPrice();
            Program.updateInfoFromDataBase();
        }

        private void поАддресуМестонахожденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByAdress();
            Program.updateInfoFromDataBase();
        }

        private void поВидуНедвижимостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByPropertyType();
            Program.updateInfoFromDataBase();
        }

        private void поДатеСделкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.g_dataBase is null) return;
            Program.g_dataBase.sortingByPropertyDate();
            Program.updateInfoFromDataBase();
        }
    }
}
