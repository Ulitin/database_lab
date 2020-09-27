using System;
using System.IO;

namespace lab2_vs2017
{
    static class Exception
    {
        static public void ExceptionString(string err)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }

        static public void ExceptionInt(string err, int min, int max)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }

        static public void ExceptionDataType(string err)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }

        static public void ExceptionDataBase(string err)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }

        static public void ExceptionIOHelper(string err)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }

        static public void ExceptionTransactionData(string err)
        {
            if (err is null) return;
            ExeptionForm exepFrom = new ExeptionForm();
            exepFrom.setTextToLabel(err);
            exepFrom.Show();
            throw new System.Exception(err);
        }
    }
}
