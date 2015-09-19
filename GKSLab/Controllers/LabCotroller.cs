using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GKSLab.Bussiness.Entities;
using GKSLab.Bussiness.Logic.Comparison_Manager;
using GKSLab.Web.ExcelIOManager;

namespace GKSLab.Controllers
{
    public class LabController : Controller
    {
        // GET: Application
        public ActionResult Lab1()
        {
            return View();
        }

        ///TODO: angular . async update of container
        /// <summary>
        /// CreateExamFromFile method. Is called after HttpPost request
        /// </summary>
        /// <param name="fileUpload">
        /// File loading
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        public ActionResult Count(HttpPostedFileBase formData)
        {
            ComparationResult result;
            HttpPostedFileBase file = HttpContext.Request.Files[0];
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
            return PartialView("_ResultTable", result);
        }
    }


}