//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace GKSLab.Business.Logic.ExcelIO_Manager
//{
//    class ConnectionString
//    {
//        public static DataSet GetExcelDataSet(HttpPostedFileBase fileUpload)
//        {
//            IExcelDataReader reader = null;
//            string fileName = fileUpload.FileName;
//            if (fileUpload != null && fileUpload.ContentLength > 0)
//            {
//                //For  .xlsx
//                if (fileName.EndsWith(".xls"))
//                {
//                    reader = ExcelReaderFactory.CreateBinaryReader(fileUpload.InputStream);
//                }
//                else if (fileName.EndsWith(".xlsx"))
//                {
//                    reader = ExcelReaderFactory.CreateOpenXmlReader(fileUpload.InputStream);
//                }

//                else
//                {
//                    //Throw exception for things you cannot correct
//                    throw new Exception("The file to be processed is not an Excel file");
//                }
//            }
//            if (reader != null)
//            {
//                var set = reader.AsDataSet();
//                return set;

//            }
//            else return null;
//        }

//        private static DataTableCollection GetExcelWorkSheet(HttpPostedFileBase fileUpload)
//        {
//            //отримаємо всі листи екселя з книжки
//            var dataSet = GetExcelDataSet(fileUpload);
//            var workSheet = dataSet.Tables;
//            if (workSheet == null)
//            {
//                throw new Exception("The worksheetes does not exist, has an incorrect name, or does not have any data in the worksheet");
//            }
//            return workSheet;
//        }
//    }
//}
