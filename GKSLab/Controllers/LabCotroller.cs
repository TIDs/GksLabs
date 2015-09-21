using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GKSLab.Bussiness.Entities;
using GKSLab.Bussiness.Logic.Comparison_Manager;
using GKSLab.Models.ViewModels;
using GKSLab.Web.ExcelIOManager;

namespace GKSLab.Controllers
{
    public class LabController : Controller
    {
        // GET: Application
        public ActionResult Lab1(string id)
        {
            return View();
        }

        // ти що вибирав, синхронізацію чи  sync зараз я спробую щось в себе мб змінити спробуй ще раз синхронізувати
        ///TODO: angular . async update of container
        /// <summary>
        /// CreateExamFromFile method. Is called after HttpPost request
        /// </summary>
        /// <param name="model">
        /// File loading
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        public ActionResult Count(InputMatrixViewModel model)
        {
            ComparationResult result;
            var matrix = new InputMatrixViewModel();
            matrix.MatrixList = new List<RowItem>();
            int i = 0;
            foreach (var rowItem in model.MatrixList)
            {
                //matrix.MatrixList = new List<RowItem>();
                var rowlist = new RowItem();
                rowlist.Row = new List<string>();
                foreach (var item in rowItem.Row)
                {
                    if (String.IsNullOrWhiteSpace(item))
                        continue;
                    if (rowlist.Row.Contains(item))
                    {
                        ViewBag.Message = "Були найдені повторювані операції в одному з рядків";
                        return View("Lab1");
                    }
                    rowlist.Row.Add(item);
                }
                matrix.MatrixList.Add(rowlist);
            }
            //HttpPostedFileBase file = HttpContext.Request.Files[0];
            try
            {
                //var inputData = ExcelReader.Read(fileUpload);
                result = ComparisonManager.CompareDetails(matrix.MatrixList.Select(item => item.Row).ToList());

            }
            catch (Exception e)
            {
                string error = e.Message;
                Debug.Write(e.Message);
                return View(error);
            }
            //returning partial view
            return View("Result", result);
        }
        [HttpPost]
        public ActionResult FileRead(HttpPostedFileBase file)
        {
            ComparationResult result;
            //HttpPostedFileBase file = HttpContext.Request.Files[0];
            try
            {
                var inputData = ExcelReader.Read(file);
                result = ComparisonManager.CompareDetails(inputData);
            }
            catch (Exception e)
            {
                string error = e.Message;
                Debug.Write(e.Message);
                return View(error);
            }
            //returning partial view
            return View("Result", result);
        }
    }


}