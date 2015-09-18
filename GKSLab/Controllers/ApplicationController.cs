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
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Lab1()
        {
            return View();
        }
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
        public ActionResult Lab1(HttpPostedFileBase fileUpload)
        {
            try
            {
                var inputData = ExcelReader.Read(fileUpload);
                ComparationResult result = ComparisonManager.CompareDetails(inputData);
            }
            catch (Exception e)
            {
                string error = e.Message;
                Debug.Write(e.Message);
                return View(error);
            }
            //returning partial view
            return View();
        }
    }

    
}