using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab2_vs2017
{
    static class Program
    {
        public static DataBase g_dataBase;
        public static string g_path;
        public static DataBaseWorker g_form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            g_dataBase = null;
            g_path = "-";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            g_form = new DataBaseWorker();
            g_form.Text = "Database Worker (" + g_path + ")";
            Application.Run(g_form);
        }

        static public void updateInfoFromDataBase()
        {
            if (g_path is null)
            {
                g_form.Text = "Database Worker (-)";
            }
            else
            {
                g_form.Text = "Database Worker (" + Path.GetFileName(g_path) + ")";
            }
            if (g_dataBase is null)
            {
                if(!(Program.g_form.dataGridView1 is null))
                {
                    while (Program.g_form.dataGridView1.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in Program.g_form.dataGridView1.Rows)
                        {
                            try
                            {
                                Program.g_form.dataGridView1.Rows.Remove(row);
                            }
                            catch (System.Exception e)
                            {
                                //exeption
                            }
                        }
                    }
                }
            }
            else if (g_dataBase.diff(ref Program.g_form.dataGridView1))
            {
                g_dataBase.set(ref Program.g_form.dataGridView1);
            }
        }
    }
}
