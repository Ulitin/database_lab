using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace lab2_vs2017
{
    class DataBase
    {
        private TransactionData[] m_transactionDataArray;
        private int m_size;
        private bool m_isDiffRAM2CSV;

        public bool neededSave()
        {
            return m_isDiffRAM2CSV;
        }

        public DataBase(int maxSize)
        {
            m_isDiffRAM2CSV = false;
            m_size = 0;
            m_transactionDataArray = new TransactionData[maxSize];
        }
        public int dataBaseSize()
        {
            if (m_size < 0)
            {
                Console.WriteLine("Critical error! Error to data base size.");
                return 0;
            }
            return m_size;
        }

        public bool add(TransactionData data)
        {
            /*
            if(search(data) != -1 )
            {
                Console.WriteLine("This information was added.");
                return false;
            }
            */

            if (m_size >= m_transactionDataArray.Length)
            {
                TransactionData[] tmp = new TransactionData[m_size * 2];
                for(int i = 0; i < m_size; i++)
                {
                    tmp[i] = m_transactionDataArray[i];
                }
                m_transactionDataArray = tmp;
            }
            m_transactionDataArray[m_size] = data;
            m_size++;

            return true;
        }

        public int search(TransactionData data)
        {
            for(int i = 0; i < m_size; i ++)
            {
                if (m_transactionDataArray[i] == data) return i;
            }
            return -1;
        }

        public int searchNext(TransactionData data, int start)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i] == data) return i;
            }
            return -1;
        }

        public int search(int index)
        {
            if (index < m_size) return index;
            return -1;
        }

        public int searchToName(string name, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_name == name) return i;
            }
            return -1;
        }

        public int searchToPhone(string phone, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_phone == phone) return i;
            }
            return -1;
        }

        public int searchToAdress(string adress, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_adress == adress) return i;
            }
            return -1;
        }

        public int searchToPropertyDate(DateTime propertyDate, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_propertyDate == propertyDate) return i;
            }
            return -1;
        }

        public int searchToPropertyDate(DateTime startTime, DateTime endTime, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_propertyDate >= startTime &&
                    m_transactionDataArray[i].m_propertyDate <= endTime) return i;
            }
            return -1;
        }

        public int searchToPropertyType(string propertyType, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_propertyType == propertyType) return i;
            }
            return -1;
        }

        public int searchToPrice(double price, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_price == price) return i;
            }
            return -1;
        }

        public int searchToPrice(double min, double max, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].m_price >= min &&
                    m_transactionDataArray[i].m_price <= max) return i;
            }
            return -1;
        }

        public bool swap(int index, TransactionData data)
        {
            if(index >= m_size || index < 0)
            {
                Console.WriteLine("Incorrect index.");
                return false;
            }
            m_transactionDataArray[index] = data;
            return true;
        }

        public ref TransactionData get(int index)
        {
            return ref m_transactionDataArray[index];
        }

        public bool delete(TransactionData data)
        {
            if (m_size <= m_transactionDataArray.Length)
            {
                m_size--;
                m_transactionDataArray = m_transactionDataArray.Where(val => val != data).ToArray();
                return true;
            }
            else
            {
                Console.WriteLine("Oversized fixed data limit.");
            }
            return false;
        }

        public bool delete(int index)
        {
            if(search(index) == -1)
            {
                Console.WriteLine("Record with such number does not exist");
                return false;
            }
            return delete(m_transactionDataArray[index]);
        }

        public void sortingByName(bool ascending = true) 
        {
            for(int i = 0; i < m_size - 1; i++)
            {
                for(int j = 0; j < m_size - i - 1; j++)
                {
                    if(ascending ^ (m_transactionDataArray[j].m_name.CompareTo(m_transactionDataArray[j + 1].m_name) == 1))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j + 1], ref m_transactionDataArray[j]);
                    }
                }
            }
        }

        public void sortingByAdress(bool ascending = true)
        {
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].m_adress.CompareTo(m_transactionDataArray[j + 1].m_adress) == 1))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }
        }

        public void sortingByPhone(bool ascending = true)
        {
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].m_phone.CompareTo(m_transactionDataArray[j + 1].m_phone) == 1))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }
        }

        public void sortingByPropertyType(bool ascending = true)
        {
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].m_propertyType.CompareTo(m_transactionDataArray[j + 1].m_propertyType) == 1))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }
        }

        public void sortingByPropertyDate(bool ascending = true)
        {
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].m_propertyDate.CompareTo(m_transactionDataArray[j + 1].m_propertyDate) == 1))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }
        }

        public void sortingByPrice(bool ascending = true)
        {
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].m_price > m_transactionDataArray[j + 1].m_price))
                    {
                        TransactionData.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }
        }

        public bool move(TransactionData elem, int index)
        {
            if(index > m_size || index <= 0)
            {
                Console.WriteLine("Data base have not data with " + index +" index");
                return false;
            }
            m_transactionDataArray[index - 1] = elem;
            Console.WriteLine("Rewrited data with " + index + " index");
            return true;
        }

        public void print(int index)
        {
            if (index > m_size || index <= 0)
            {
                Console.WriteLine("Data base have not data with " + index + " index");
            }
            else
            {
                Console.WriteLine("----------" + (index + 1) + " element----------");
                Console.WriteLine("Name: "          + m_transactionDataArray[index].m_name);
                Console.WriteLine("Phone: "         + m_transactionDataArray[index].m_phone);
                Console.WriteLine("Adress: "        + m_transactionDataArray[index].m_adress);
                Console.WriteLine("Property type: " + m_transactionDataArray[index].m_propertyType);
                Console.WriteLine("Price: "         + m_transactionDataArray[index].m_price.ToString());
                Console.WriteLine("Property date: " + m_transactionDataArray[index].m_propertyDate);
            }
        }
        public void printAllData()
        {
            if (m_size == 0)
            {
                Console.WriteLine("Data base is empty.");
                return;
            }
            else if (m_size < 0)
            {
                Console.WriteLine("Critical error! Error to data base size.");
                return;
            }

            for (int i = 0; i < m_size; i++)
            {
                Console.WriteLine("----------" + (i + 1) + " element----------" );
                Console.WriteLine("Name: "          + m_transactionDataArray[i].m_name);
                Console.WriteLine("Phone: "         + m_transactionDataArray[i].m_phone);
                Console.WriteLine("Adress: "        + m_transactionDataArray[i].m_adress);
                Console.WriteLine("Property type: " + m_transactionDataArray[i].m_propertyType);
                Console.WriteLine("Price: "         + m_transactionDataArray[i].m_price.ToString());
                Console.WriteLine("Property date: " + m_transactionDataArray[i].m_propertyDate);
            }
        }

        public bool diff(ref System.Windows.Forms.DataGridView grid)
        {
            int sizeRow = grid.RowCount;
            int sizeColumn = grid.ColumnCount;
            if(sizeColumn != TransactionData.memberSize)
            {
                //exeption
            }
            if(sizeRow != m_size)
            {
                return true;
            }
            for(int i = 0; i < sizeRow; i++)
            {
                if (grid.Rows[i] is null)
                {
                    if ((m_transactionDataArray is null) ||
                        (!(m_transactionDataArray[i] is null)))
                    {
                        return true;
                    }
                }
                else
                {
                    DataGridViewRow row = grid.Rows[i];
                    TransactionData tmp = IOHelper.createTransactionDataFromRow(row);
                    if (tmp != m_transactionDataArray[i])
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public void set(ref System.Windows.Forms.DataGridView grid)
        {
            while (grid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    try
                    {
                        grid.Rows.Remove(row);
                    }
                    catch (System.Exception e)
                    {
                        //exeption
                    }
                }
            }
            if(m_transactionDataArray is null)
            {
                return;
            }
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i] is null)
                {
                    //exeption
                }
                String[] row = { m_transactionDataArray[i].m_name, m_transactionDataArray[i].m_phone,
                                Convert.ToString(m_transactionDataArray[i].m_price), m_transactionDataArray[i].m_adress,
                                m_transactionDataArray[i].m_propertyType , Convert.ToString(m_transactionDataArray[i].m_propertyDate)};
                grid.Rows.Add(row);
                grid.AllowUserToAddRows = false;
            }
        }
    }

}
