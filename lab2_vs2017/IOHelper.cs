using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace lab2_vs2017
{
    class IOHelper
    {
        public static TransactionData createTransactionDataWithUser()
        {
            try
            {
                Console.WriteLine("Input name");
                string name = strValid(Console.ReadLine());
                Console.WriteLine("Input adress");
                string adress = strValid(Console.ReadLine());
                Console.WriteLine("Input phone");
                string phone = phoneValid(Console.ReadLine());
                Console.WriteLine("Input price");
                int price = priceValid(Console.ReadLine());
                Console.WriteLine("Input property date");
                DateTime propertyDate = dataValid(Console.ReadLine());
                Console.WriteLine("Input property type");
                string propertyType = strValid(Console.ReadLine());
                return new TransactionData(name, phone, adress, propertyType, price, propertyDate);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;
        }

        public static TransactionData createTransactionDataFromRow(DataGridViewRow row)
        {
            try
            {
                string name   = strValid(Convert.ToString(row.Cells[0].Value));
                string adress = strValid(Convert.ToString(row.Cells[3].Value));
                string phone  = phoneValid(Convert.ToString(row.Cells[1].Value));
                int price     = priceValid(Convert.ToString(row.Cells[2].Value));
                DateTime propertyDate = dataValid(Convert.ToString(row.Cells[5].Value));
                string propertyType = strValid(Convert.ToString(row.Cells[4].Value));
                return new TransactionData(name, phone, adress, propertyType, price, propertyDate);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;
        }

        public static TransactionData createTransactionData(string _name, string _phone,
                                                            string _adress, string _propertyType,
                                                            string _price, string _propertyDate)
        {
            try
            {
                string name = strValid(_name);
                string adress = strValid(_adress);
                string phone = phoneValid(_phone);
                int price = priceValid(_price);
                DateTime propertyDate = dataValid(_propertyDate);
                string propertyType = strValid(_propertyType);
                return new TransactionData(name, phone, adress, propertyType, price, propertyDate);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;
        }

        public static DataBase createDataBaseFromFile(string path)
        {
            DataBase res = new DataBase(0);
            string correctPath = strValid(path);
            string line;
            try
            {
                StreamReader sr = new StreamReader(correctPath);
                res = new DataBase(sizeValid(sr.ReadLine()));
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] words = line.Split(new char[] { ';' });
                    if (words.Length != TransactionData.memberSize)
                    {
                        Exception.ExceptionIOHelper("Incorrect data base to " + correctPath);
                        return res;
                    }

                    TransactionData record = new TransactionData(strValid(words[0]), phoneValid(words[2]),
                        strValid(words[1]), strValid(words[5]), priceValid(words[3]), dataValid(words[4]));
                    res.add(record);

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch(System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return res;
        }

        public static void createFileFromDataBase(DataBase dataBase, string path)
        {
            string correctPath = path;
            try
            {
                StreamWriter sw = new StreamWriter(correctPath);
                sw.WriteLine(dataBase.dataBaseSize());
                for (int i = 0; i < dataBase.dataBaseSize(); i++)
                {
                    TransactionData record = dataBase.get(i);
                    sw.WriteLine(record.m_name  + ";" + record.m_adress + ";" +
                                 record.m_phone + ";" + record.m_price  + ";" +
                                 record.m_propertyDate + ";" + record.m_propertyType);
                }
                sw.Close();
            }
            catch(System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public static bool isReadFile(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public static bool isWrittenFile(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Write).Dispose();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public static string strValid(string str)
        {
            if(str == null)
            {
                Exception.ExceptionString("Input string is null.");
            }
            else if(str.Length == 0)
            {
                Exception.ExceptionString("Input string is empty in string valid.");
            }
            return str;
        }

        public static DateTime dataValid(string str)
        {
            DateTime res = new DateTime();
            try
            {
                if (str == null)
                {
                    Exception.ExceptionString("Input string is null.");
                }
                else if (str.Length == 0)
                {
                    Exception.ExceptionString("Input string is empty in string valid.");
                }
                res = Convert.ToDateTime(str);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Exception.ExceptionDataType(str);
            }
            return res;
        }

        public static int priceValid(string str)
        {
            if (str == null)
            {
                Exception.ExceptionInt("Input string is null", 0, Int32.MaxValue);
            }
            else if (str.Length == 0)
            {
                Exception.ExceptionString("Input string is empty in price valid.");
            }
            else if (str[0] == '-')
            {
                Exception.ExceptionInt("The price cannot be negative.", 0, Int32.MaxValue);
            }

            int startIndex = 0;
            for (; startIndex < str.Length && str[startIndex] == '0'; startIndex++) {}
            if (startIndex == str.Length) return 0;
            string trueStr = str.Substring(startIndex);

            if (trueStr.Length > Int32.MaxValue.ToString().Length)
            {
                Exception.ExceptionInt("Price overflow.", 0, Int32.MaxValue);
            }
            if (trueStr.Length == Int32.MaxValue.ToString().Length &&
                string.Compare(trueStr, Int32.MaxValue.ToString()) == 1)
            {
                Exception.ExceptionInt("Price overflow.", 0, Int32.MaxValue);
            }

            for (int i = 0; i < trueStr.Length; i++)
            {
                if(trueStr[i] != '0' && trueStr[i] != '1' && trueStr[i] != '2' && trueStr[i] != '3' &&
                   trueStr[i] != '4' && trueStr[i] != '5' && trueStr[i] != '6' && trueStr[i] != '7' &&
                   trueStr[i] != '8' && trueStr[i] != '9')
                {
                    Exception.ExceptionInt("Input string is not a number", 0, Int32.MaxValue);
                }
            }

            return Convert.ToInt32(trueStr);
        }

        public static int sizeValid(string str)
        {
            if (str == null)
            {
                Exception.ExceptionInt("Input string is null", 0, Int32.MaxValue);
            }
            else if (str.Length == 0)
            {
                Exception.ExceptionString("Input string is empty in size valid.");
            }
            else if (str[0] == '-')
            {
                Exception.ExceptionInt("The size cannot be negative.", 0, Int32.MaxValue);
            }

            int startIndex = 0;
            for (; startIndex < str.Length && str[startIndex] == '0'; startIndex++) { }
            if (startIndex == str.Length) return 0;
            string trueStr = str.Substring(startIndex);

            if (trueStr.Length > Int32.MaxValue.ToString().Length)
            {
                Exception.ExceptionInt("Price overflow.", 0, Int32.MaxValue);
            }
            if (trueStr.Length == Int32.MaxValue.ToString().Length && 
                string.Compare(trueStr, Int32.MaxValue.ToString()) == 1)
            {
                Exception.ExceptionInt("Price overflow.", 0, Int32.MaxValue);
            }

            for (int i = 0; i < trueStr.Length; i++)
            {
                if (trueStr[i] != '0' && trueStr[i] != '1' && trueStr[i] != '2' && trueStr[i] != '3' &&
                   trueStr[i] != '4' && trueStr[i] != '5' && trueStr[i] != '6' && trueStr[i] != '7' &&
                   trueStr[i] != '8' && trueStr[i] != '9')
                {
                    Exception.ExceptionInt("Input string is not a number", 0, Int32.MaxValue);
                }
            }

            return Convert.ToInt32(trueStr);
        }

        public static string phoneValid(string str)
        {
            if (str == null)
            {
                Exception.ExceptionString("Input string is null");
            }
            else if (str.Length == 0)
            {
                Exception.ExceptionString("Input string is empty in phone valid.");
            }
            else if ((str[0] != '8' && str.Length != '+') ||
                     (str[0] == '+' && str.Length != 12) ||
                     (str[0] == '8' && str.Length != 11))
            {
                Exception.ExceptionString("Input number phone is incorrect.");
            }

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] != '0' && str[i] != '1' && str[i] != '2' && str[i] != '3' &&
                   str[i] != '4'  && str[i] != '5' && str[i] != '6' && str[i] != '7' &&
                   str[i] != '8'  && str[i] != '9')
                {
                    Exception.ExceptionString("Input string is not a number phone");
                }
            }

            return str;
        }
    }
}
