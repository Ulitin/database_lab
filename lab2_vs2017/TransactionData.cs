using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_vs2017
{
    class TransactionData
    {
        public static int memberSize = 6;
        public string m_name;
        public string m_phone;
        public string m_adress;
        public string m_propertyType;
        public DateTime m_propertyDate;
        public int m_price;

        public TransactionData(string name, string phone, string adress,
                                string propertyType, int price, DateTime propertyDate)
        {
            this.m_name = name;
            this.m_phone = phone;
            this.m_adress = adress;
            this.m_propertyDate = propertyDate;
            this.m_price = price;
            this.m_propertyType = propertyType;
        }

        public TransactionData(TransactionData data)
        {
            this.m_name = data.m_name;
            this.m_phone = data.m_phone;
            this.m_adress = data.m_adress;
            this.m_propertyDate = data.m_propertyDate;
            this.m_price = data.m_price;
            this.m_propertyType = data.m_propertyType;
        }

        public static bool operator != (TransactionData lData, TransactionData rData)
        {
            if(lData is null)
            {
                if (rData is null) return false;
                else return true;
            }
            else if(rData is null)
            {
                return true;
            }
            return lData.m_name != rData.m_name ||
                lData.m_phone != rData.m_phone ||
                lData.m_adress != rData.m_adress ||
                lData.m_propertyDate != rData.m_propertyDate ||
                lData.m_price != rData.m_price ||
                lData.m_propertyType != rData.m_propertyType;
        }

        public static bool operator == (TransactionData lData, TransactionData rData)
        {
            return !(lData != rData);
        }

        public static void swap(ref TransactionData lhs, ref TransactionData rhs)
        {
            TransactionData tmp = new TransactionData(lhs);

            lhs.m_name = rhs.m_name;
            lhs.m_phone = rhs.m_phone;
            lhs.m_adress = rhs.m_adress;
            lhs.m_propertyDate = rhs.m_propertyDate;
            lhs.m_price = rhs.m_price;
            lhs.m_propertyType = rhs.m_propertyType;

            rhs.m_name = tmp.m_name;
            rhs.m_phone = tmp.m_phone;
            rhs.m_adress = tmp.m_adress;
            rhs.m_propertyDate = tmp.m_propertyDate;
            rhs.m_price = tmp.m_price;
            rhs.m_propertyType = tmp.m_propertyType;
        }

    }

}
