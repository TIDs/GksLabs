using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GKSLab.Bussiness.Entities;
using GKSLab.Bussiness.Logic.Comparison_Manager;
using GKSLab.Bussiness.Logic.Graph_Manager;
using GKSLab.Bussiness.Logic.Groups_Manager;
using GKSLab.Bussiness.Logic.Modules_Manager;
using GKSLab.Models.ViewModels;
using GKSLab.Web.ExcelIOManager;
using GKSLab.Bussiness.Entities.Graph;

namespace GKSLab.Controllers
{
    public class LabController : Controller
    {
        // GET: Application
        public ActionResult Lab1(string id)
        {
            return View();
        }

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
            var uniqueElements = 0;
            List<List<string>> inputData;
            List<List<int>> groups = new List<List<int>>();
            List<List<int>> redistributionsGroup = new List<List<int>>();
            List<HashSet<string>> groupsWithStringElement;
            var matrix = new InputMatrixViewModel();
            matrix.MatrixList = new List<RowItem>();
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
                inputData = new List<List<string>>(matrix.MatrixList.Select(item => item.Row).ToList());
                uniqueElements = ComparisonManager.UniqueElementsAmount(inputData);
                result = ComparisonManager.CompareDetails(matrix.MatrixList.Select(item => item.Row).ToList());
                groups = DevisionGroupsManager.CreateGroups(result);
                groupsWithStringElement = RedistributionGroupsManager.CreateGroupsWithStringElement(inputData, groups);
                var groupForRedistributions = new List<List<int>>();
                groups.ForEach(x => groupForRedistributions.Add(x));
                redistributionsGroup = RedistributionGroupsManager.RedistributionGroups(inputData, groupForRedistributions, groupsWithStringElement);
            }
            catch (Exception e)
            {
                string error = e.Message;
                Debug.Write(e.Message);
                return View(error);
            }
            //returning partial view
            ViewBag.InputData = inputData;
            ViewBag.Unique = uniqueElements;
            ViewBag.Groups = groups;
            ViewBag.GroupString = groupsWithStringElement;
            ViewBag.RedistributedGroups = redistributionsGroup;
            return View("Result", result);
        }
        [HttpPost]
        public ActionResult FileRead(HttpPostedFileBase file)
        {

            ComparationResult result;
            var uniqueElements = 0;
            List<List<string>> inputData = new List<List<string>>();
            List<List<int>> groups = new List<List<int>>();
            List<List<int>> redistributionsGroup = new List<List<int>>();
            List<HashSet<string>> groupsWithStringElement;
            //HttpPostedFileBase file = HttpContext.Request.Files[0];
            try
            {
                inputData = ExcelReader.Read(file);
                uniqueElements = ComparisonManager.UniqueElementsAmount(inputData);
                result = ComparisonManager.CompareDetails(inputData);
                groups = DevisionGroupsManager.CreateGroups(result);
                groupsWithStringElement = RedistributionGroupsManager.CreateGroupsWithStringElement(inputData, groups);
                var groupWithString = new List<HashSet<string>>(groupsWithStringElement);
                var groupForRedistributions = new List<List<int>>();
                groups.ForEach(x => groupForRedistributions.Add(x));
                redistributionsGroup = RedistributionGroupsManager.RedistributionGroups(inputData, groupForRedistributions, groupWithString);
            }
            catch (Exception e)
            {
                string error = e.Message;
                Debug.Write(e.Message);
                return View(error);
            }
            //returning partial view
            ViewBag.InputData = inputData;
            ViewBag.Unique = uniqueElements;
            ViewBag.Groups = groups;
            ViewBag.GroupString = groupsWithStringElement;
            ViewBag.RedistributedGroups = redistributionsGroup;
            return View("Result", result);
        }

        /// <summary>
        /// Test Method for LAB 4
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult Test(HttpPostedFileBase file)
        {
            ComparationResult result;
            var uniqueElements = 0;
            List<List<string>> inputData = new List<List<string>>();
            List<List<int>> groups = new List<List<int>>();
            List<List<int>> redistributionsGroup = new List<List<int>>();

            ////First TEST DATA
            //inputData.Add(new List<string>(7) { "T1", "C1", "F1", "F2", "T3", "T4" });
            //inputData.Add(new List<string>(4) { "T4", "C1", "F2" });
            //inputData.Add(new List<string>(6) { "T4", "F1",  "F2" });
            //inputData.Add(new List<string>(3) {  "T1", "F2" });

            ////second test data
            inputData.Add(new List<string>(7) { "T1", "C1", "F1", "F2", "T3", "T4" });
            inputData.Add(new List<string>(4) { "T4", "C1", "F2" });
            inputData.Add(new List<string>(6) { "T4", "T3", "F2" });
            inputData.Add(new List<string>(3) { "C1", "T1" });

            //test data for find graph
            //inputData.Add(new List<string>(7) { "T1", "C1", "F1" });
            //inputData.Add(new List<string>(4) { "F1", "F2" });
            //inputData.Add(new List<string>(6) { "F2", "P1", "C1" });
            //inputData.Add(new List<string>(3) { "F1", "P1" });

            groups.Add(new List<int>() { 0, 1, 2, 3 });

            var model = new List<HashSet<string>>();
            //creating graph
            //changing index for 0-based array
            for (int index = 0; index < redistributionsGroup[0].Count; index++)
            {
                redistributionsGroup[0][index] -= 1;
            }
            var graphs = new List<Graph>();
            foreach (var redistrItem in redistributionsGroup)
            {
                var list = new HashSet<string>();
                var graph = GraphManager.Create(redistrItem, inputData);
                list = GraphManager.CreateModules(graph, list);
                model.Add(list);
            }

            //Creating simplified graph model. It's should be like '[1->2,1->4,2->3]'
            return View("Test", model: model.ToList());
        }

        public ActionResult Lab5()
        {
            List<HashSet<string>> simplifyModules = new List<HashSet<string>>();

            // primary data
            // first groups with modules
            List<List<List<string>>> groupsWithModules = new List<List<List<string>>>();
            List<List<string>> firstGroups = new List<List<string>>();
            firstGroups.Add(new List<string> { "T1", "T2", "C1" });
            firstGroups.Add(new List<string> { "T1" });
            firstGroups.Add(new List<string> { "C1" });

            // second groups with modules
            List<List<string>> secondGroups = new List<List<string>>();
            secondGroups.Add(new List<string> { "T3", "T4", "T2", "C2" });
            secondGroups.Add(new List<string> { "T1", "C1" });
            secondGroups.Add(new List<string> { "C3" });

            //third groups with modules
            List<List<string>> thirdGroups = new List<List<string>>();
            thirdGroups.Add(new List<string> { "T1" });
            thirdGroups.Add(new List<string> { "T3" });
            thirdGroups.Add(new List<string> { "C2" });

            groupsWithModules.Add(firstGroups);
            groupsWithModules.Add(secondGroups);
            groupsWithModules.Add(thirdGroups);

            simplifyModules = SimplifyModulesManager.SimplifyModules(groupsWithModules);

            ViewBag.PrimaryData = groupsWithModules;

            return View("Lab5", simplifyModules);
        }
    }
}