using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Excel;

namespace GKSLab.Web.ExcelIOManager
{
    static class ExcelReader
    {
        private static DataTableCollection GetExcelWorkSheet(HttpPostedFileBase fileUpload)
        {
            //отримаємо всі листи екселя з книжки
            var dataSet = GetExcelDataSet(fileUpload);
            var workSheet = dataSet.Tables;
            if (workSheet == null)
            {
                throw new Exception("The worksheetes does not exist, has an incorrect name, or does not have any data in the worksheet");
            }
            return workSheet;
        }
        private static DataSet GetExcelDataSet(HttpPostedFileBase fileUpload)
        {
            IExcelDataReader reader = null;
            var fileName = fileUpload.FileName;
            if (fileUpload.ContentLength > 0)
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
            var set = reader?.AsDataSet();
            return set;
        }
        public static List<List<string>> Read(HttpPostedFileBase fileUpload)
        {
            var newExamId = Guid.Empty;
            var workSheetes = GetExcelWorkSheet(fileUpload);
            var fileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
            try
            {
                //foreach (DataTable item in workSheetes)
                //{
                var item = workSheetes[0];
                return GetWorkSheetData(item);
                //}
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                throw new Exception("Some Error was happend on data reading.");
            }
        }

        private static List<List<string>> GetWorkSheetData(DataTable item)
        {
            List<List<string>> resultList = new List<List<string>>();
            var i = 0;
            try
            {
                do
                {
                    resultList.Add(item.Rows[i].ItemArray.Select(x => x.ToString())
                    .Where((s => !string.IsNullOrEmpty(s))).ToList());
                    i++;
                } while (i != item.Rows.Count);
            }
            catch (Exception)
            {
                throw;
            }
            return resultList;
        }
    }
}
