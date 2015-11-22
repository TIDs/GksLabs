using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Excel;
using GKSLab.fonts.Helpers.ExcelIO_Manager;
namespace GKSLab.fonts.Helpers.ExcelIO_Manager
{
    public static class ExcelWriter
    {
        private static string _createTable = "";
        private static string _insertQuery = "";
        private static void GenerateQueries(int dataAmount)
        {
            _insertQuery = String.Empty;
            _createTable = String.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert into [{0}] (");
            StringBuilder dataT = new StringBuilder();
            StringBuilder resultT = new StringBuilder();
            StringBuilder valuesString = new StringBuilder();
            valuesString.Append(" Values (");
            for (int i = 1; i <= dataAmount; i++)
            {
                if (dataAmount == i)
                {
                    dataT.Append(String.Format("[Data {0}])", i));
                }
                else
                    dataT.Append(String.Format("[Data {0}],", i));
            }
            for (int i = 1; i <= dataAmount; i++)
            {
                if (i == dataAmount) valuesString.Append("?)");
                else
                    valuesString.Append("?,");
            }
            sb.Append(dataT);

            sb.Append(valuesString);
            _insertQuery += sb;

            sb.Replace("Insert into ", "CREATE TABLE");
            sb.Replace("[QuestType]", "[QuestType] INT");
            sb.Replace("[Category]", "[Category] INT");
            sb.Replace(dataT.ToString(), "");
            sb.Replace(valuesString.ToString(), "");

            dataT.Clear();
            resultT.Clear();

            for (int i = 1; i <= dataAmount; i++)
            {
                if (i == dataAmount)
                    resultT.Append(String.Format("[Data {0}] VARCHAR)", i));
                else
                    resultT.Append(String.Format("[Data {0}] VARCHAR,", i));
            }
            sb.Append(resultT);
            _createTable += sb;
            Debug.Write(_insertQuery);
            Debug.WriteLine(_createTable);
        }
        private static string GetConnectionString(string fullPath)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = fullPath;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }
            return sb.ToString();
        }

        public static void Write(string title, List<List<string>> data, string path)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString();
                string connectionString = GetConnectionString(path);
                //GenerateQueries(4);
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    //cmd = new OleDbCommand(String.Format(insertQuery, "table1$"), conn);
                    GenerateQueries(data.Count);
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format(_createTable, title);
                    cmd.ExecuteNonQuery();
                    OleDbCommand wcmd = new OleDbCommand(String.Format(_insertQuery, title), conn);

                    int i = 0;
                    data = data.Pivot(false);
                    foreach (var itemData in data)
                    {
                        i++;
                        ResolveParameters(wcmd, itemData, i);

                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.InnerException);
                Debug.Write(e.Message);
            }
        }

        private static void ResolveParameters(OleDbCommand cmd, List<string> strList, int i)
        {
            cmd.Parameters.Clear();
            int rowN = 0;
            foreach (var item in strList)
            {
                rowN++;
                OleDbParameter parameter = cmd.Parameters.AddWithValue(String.Format("[Data {0}]", rowN), item);
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Source);
                Debug.WriteLine(e.Data);
            }

        }

        public static DataSet GetExcelDataSet(HttpPostedFileBase fileUpload)
        {
            IExcelDataReader reader = null;
            string fileName = fileUpload.FileName;
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                //For  .xlsx
                if (fileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(fileUpload.InputStream);
                }
                else if (fileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(fileUpload.InputStream);
                }

                else
                {
                    //Throw exception for things you cannot correct
                    throw new Exception("The file to be processed is not an Excel file");
                }
            }
            if (reader != null)
            {
                var set = reader.AsDataSet();
                return set;

            }
            else return null;
        }
    }
    public static class MyExtensions
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    public static class Extesion
    {
        public static List<List<T>> Pivot<T>(this List<List<T>> inputLists, bool removeEmpty, T defaultVal = default(T))
        {
            if (inputLists == null) throw new ArgumentNullException("inputLists");
            if (removeEmpty && !object.Equals(defaultVal, default(T))) throw new ArgumentException("You cannot provide a default value and removeEmpty at the same time!", "removeEmpty");

            int maxCount = inputLists.Max(l => l.Count);
            List<List<T>> outputLists = new List<List<T>>(maxCount);
            for (int i = 0; i < maxCount; i++)
            {
                List<T> list = new List<T>();
                outputLists.Add(list);
                for (int index = 0; index < inputLists.Count; index++)
                {
                    List<T> inputList = inputLists[index];
                    bool listSmaller = inputList.Count <= i;
                    if (listSmaller)
                    {
                        if (!removeEmpty)
                            list.Add(defaultVal);
                    }
                    else
                        list.Add(inputList[i]);
                }
            }
            return outputLists;
        }
    }

}