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
using GKSLab.Models.ViewModels;
using GKSLab.Web.ExcelIOManager;
using GKSLab.Bussiness.Entities.Graph;
using GKSLab.fonts.Helpers.ExcelIO_Manager;

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
            //ViewBag.InputData = inputData;
            //ViewBag.Unique = uniqueElements;
            //ViewBag.Groups = groups;
            //ViewBag.GroupString = groupsWithStringElement;
            //ViewBag.RedistributedGroups = redistributionsGroup;

            var graphModel = new GraphLabViewModel();
            graphModel.ResultingMatrix = result.ResultingMatrix;
            graphModel.InputData = inputData;
            graphModel.Unique = uniqueElements;
            graphModel.Groups = groups;
            graphModel.GroupString = groupsWithStringElement;
            graphModel.RedistributedGroups = redistributionsGroup;
            return View("Result", graphModel);
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

            //ViewBag.InputData = inputData;
            //ViewBag.Unique = uniqueElements;
            //ViewBag.Groups = groups;
            //ViewBag.GroupString = groupsWithStringElement;
            //ViewBag.RedistributedGroups = redistributionsGroup;

            var graphModel = new GraphLabViewModel();
            graphModel.ResultingMatrix = result.ResultingMatrix;
            graphModel.InputData = inputData;
            graphModel.Unique = uniqueElements;
            graphModel.Groups = groups;
            graphModel.GroupString = groupsWithStringElement;
            graphModel.RedistributedGroups = redistributionsGroup;
            _currentGraph = new СreationGraphModel()
            {
                Groups = groups,
                InputData = inputData,
                RedistributedGroups = redistributionsGroup
            };
            return View("Result", graphModel);
        }

        private static СreationGraphModel _currentGraph;

        public ActionResult Lab4()
        {
            return View();
        }
        /// <summary>
        /// Test Method for LAB 4
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        public ActionResult Test()
        {

            ////second test data
            //inputData.Add(new List<string>(7) { "T1", "C1", "F1", "F2", "T3", "T4" });
            //inputData.Add(new List<string>(4) { "T4", "C1", "F2" });
            //inputData.Add(new List<string>(6) { "T4", "T3", "F2" });
            //inputData.Add(new List<string>(3) { "C1", "T1" });


            //groups.Add(new List<int>() { 0, 1, 2, 3 });
            //groups.Add(new List<int>() { 1, 2 });
            //groups.Add(new List<int>() { 3,0 });

            //redistributionsGroup.Add(new List<int>() {1,5,7,9,8});
            //redistributionsGroup.Add(new List<int>() { 3, 4, 7, 9, 2 });
            //redistributionsGroup.Add(new List<int>() { 12, 0,2, 7, 6 });


            var graphModel = new List<HashSet<string>>();
            //creating graph
<<<<<<< HEAD
            //for (int i = 0; i < _currentGraph.RedistributedGroups.Count; i++)
            //{
            //    for (int index = 0; index < _currentGraph.RedistributedGroups[0].Count; index++)
            //    {
            //        _currentGraph.RedistributedGroups[0][index] -= 1;
            //    }
            //}
           
=======
            for (int i = 0; i < _currentGraph.RedistributedGroups.Count; i++)
            {
                for (int index = 0; index < _currentGraph.RedistributedGroups[0].Count; index++)
                {
                    _currentGraph.RedistributedGroups[0][index] -= 1;
                }
            }

>>>>>>> 7fb64ad77ca6bcd152fd3d2a153d35f7bb80ff2a
            foreach (var redistrItem in _currentGraph.RedistributedGroups)
            {
                var list = new HashSet<string>();
                //creating graph
                //changing index for 0-based array
               
                var graph = GraphManager.Create(redistrItem, _currentGraph.InputData);
                list = GraphManager.CreateModules(graph, list);
                graphModel.Add(list);
            }


            //Creating simplified graph model. It's should be like '[1->2,1->4,2->3]'
            return View("Test", model: graphModel.ToList());
        }

        public ActionResult TestFile(HttpPostedFileBase file)
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
    }
}