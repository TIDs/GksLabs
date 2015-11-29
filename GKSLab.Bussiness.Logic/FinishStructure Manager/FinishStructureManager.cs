using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using GKSLab.Bussiness.Logic.Graph_Manager;
using GKSLab.Bussiness.Entities.Graph;
 
namespace GKSLab.Bussiness.Logic.FinishStructure_Manager
{
    public static class FinishStructureManager
    {

        public static List<List<string>> combinations;
        
        public static bool[] used;

        /// <summary>
        /// Create finish structure automatic system
        /// </summary>
        /// <param name="simplifyModules">Simplify modules</param>
        /// <param name="primaryData">Primary data</param>
        public static List<List<string>> CreateFinishStructure(Dictionary<int, List<string>> singleModule, Dictionary<int, List<string>> singleOperation, List<string> simplifyModules, List<HashSet<string>> statesFinishStructures)
        {
            // containt order modules and amount reverce connections
            List<List<string>> resultOptimization = new List<List<string>>();

            // first and last elemеnts which in most primaryData in first and last positions 
            List<string> firstAndLastElements = new List<string>();

            //
            List<string> optimalGraph = new List<string>();

            //finish strucrute
            Graph graph = new Graph();

            // amount reverse connections
            int amountReverseConnection = 0;

             // find first and last element that is most frequent in position singleOperation[i].Value[0] and singleOperation[i].Value[Count - 1]
            firstAndLastElements = FindFirstAndLastElements(singleOperation);

            // find first and last module in finish structured and shift him to accordyngly first and last position
            FindFirstModule(singleModule, simplifyModules, firstAndLastElements[0]);
            FindLastModule(singleModule,simplifyModules, firstAndLastElements[1], singleOperation);

            //create primary graph
            graph = GraphManager.Create(new List<int> { 0 }, new List<List<string>> { simplifyModules });
            // remove all childrens and parents in node
            graph.Roots.ForEach(x => x.Children.Clear());
            graph.Roots.ForEach(x => x.Parents.Clear());

            // create finish structure and counting the number of reverse
            amountReverseConnection = CreateFinishStructure(graph, singleOperation, singleModule);

            //save first state graph
            statesFinishStructures.Add(new HashSet<string>() { graph.ToString() });

            optimalGraph = FindOptimalStructure(simplifyModules, singleOperation);
            simplifyModules.Add(amountReverseConnection.ToString());

            resultOptimization.Add(simplifyModules);
            resultOptimization.Add(optimalGraph);

            return resultOptimization;
        }

        /// <summary>
        /// Find first module in finish structure
        /// </summary>
        /// <param name="dictModule"> Dictionary devision single module</param>
        /// <param name="simplifModule">Simplify modules</param>
        /// <param name="element">Element in first module</param>
        public static void FindFirstModule(Dictionary<int, List<string>> dictModule, List<string> simplifModule, string element)
        {
            for (int i = 0; i < dictModule.Count; i++)
            {
                 // find modules that contains first element and shift him in first position
                if (dictModule[i].Contains(element))
                {
                    // modules which element devision on single string element
                     var tempDictionary = dictModule[0];
                     dictModule[0] = dictModule[i];
                     dictModule[i] = tempDictionary;
 
                    // modules which element together
                     var tempList = simplifModule[0];
                     simplifModule[0] = simplifModule[i];
                     simplifModule[i] = tempList;
                }
            }
        }
      
        /// <summary>
        /// Find last module in finish structure
        /// </summary>
        /// <param name="dictModule"> Dictionary devision single module</param>
        /// <param name="simplifModule">Simplify modules</param>
        /// <param name="element">Element in last module</param>
        public static void FindLastModule(Dictionary<int, List<string>> dictModule, List<string> simplifModule, string element, Dictionary<int, List<string>> prData)
        {
            int amountElement = dictModule.Count - 1;
            List<string> findNewLastElement = new List<string>();
            int amount = 0;

            foreach(var item in prData)
            {
                amount = item.Value.Count - 1;
                findNewLastElement.Add(item.Value[amount]);   
            }

            for (int i = 0; i < dictModule.Count; i++)
            {
                 // find modules that contains last element and shift him in last position
                if (dictModule[i].Contains(element) && i != amountElement)
                {
                    if (i == 0)
                    {
                        element = FindGivenElement(findNewLastElement, element);
                        i = -1;
                        continue;
                    }
                    // modules which element devision on single string element
                    var tempDictionary = dictModule[amountElement];
                    dictModule[amountElement] = dictModule[i];
                    dictModule[i] = tempDictionary;

                    // modules which element together
                    var tempList = simplifModule[amountElement];
                    simplifModule[amountElement] = simplifModule[i];
                    simplifModule[i] = tempList;
                }
            }
        }

        /// <summary>
        /// Find first and last that a lot of see 
        /// </summary>
        /// <param name="prData">primaryData</param>
        public static List<string> FindFirstAndLastElements(Dictionary<int, List<string>> prData)
        {
            List<string> result = new List<string>();
            List<string> listFirstElemnts = new List<string>();
            List<string> listLastElements = new List<string>();

            foreach( var dict in prData)
            {
                var amount = dict.Value.Count;
                // add to list all first elements
                listFirstElemnts.Add(dict.Value[0]);

                //add to list all last elements
                listLastElements.Add(dict.Value[amount - 1]);
            }

            // find element that often found in first position
            result.Add(FindGivenElement(listFirstElemnts));

            //find element that often found in last position
            result.Add(FindGivenElement(listLastElements));

            return result;
        }

        /// <summary>
        /// Find given element in list 
        /// </summary>
        /// <param name="listForFind"></param>
        /// <returns>Element that is most frequent</returns>
        private static string FindGivenElement(List<string> listForFind, params string[] repetedElement)
        {
            string element;
            string findedElement = "";
            int amountOneElementInList = 0;
            string repetElement = "";

            foreach (var repElem in repetedElement)
            {
                repetElement = repElem;
            }
            
            for (int i = 0; i < listForFind.Count; i++)
            {
                element = listForFind[i];
                if (i == 0)
                {
                    // choose the first element as most frequent
                    amountOneElementInList = listForFind.Count(x => x == element && x != repetElement);
                    findedElement = element;
                }
                // check if exist element that is more frequent than previosely
                else if (amountOneElementInList < listForFind.Count(x => x == element && x != repetElement))
                {
                    amountOneElementInList = listForFind.Count(x => x == element);
                    findedElement = element;
                }
            }

            return findedElement;
        }

        /// <summary>
        /// Devision string with many elements to array string element
        /// </summary>
        /// <param name="prData">Primary data</param>
        public static Dictionary<int, List<string>> ConvertString(List<string> prData)
        {
            List<string> devisionOperations;

            // devision string by pattern (string element -> some element that dont equal null)
            string pattern = @"(\w)(\d+)";

            Dictionary<int, List<string>> singleOperation = new Dictionary<int, List<string>>();

            for(int i = 0; i< prData.Count; i++)
            {
                devisionOperations = new List<string>();
                foreach(var operation in Regex.Matches(prData[i], pattern))
                {
                    devisionOperations.Add(operation.ToString());
                }
                singleOperation.Add(i, devisionOperations);
            }

            return singleOperation;
        }

        /// <summary>
        /// Create finish structure 
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="primData">Primary data</param>
        /// <param name="singleSimplModules">Dictionary simplify modules that devision to single modules</param>
        /// <param name="moduleInGraph">Modules in graph that consist in simple modules</param>
        /// <returns> Amount reverse connection</returns>
        private static int CreateFinishStructure(Graph graph, Dictionary<int, List<string>> primData, Dictionary<int, List<string>> singleSimplModules)
        {
            int numberModuleNext = 0;
            Node<string> cuurrentNode = new Node<string>();
            int amountFeedbackConnections = 0;
            int orderModules = 0;

            foreach(var data in primData)
            {
                for (int i = 0; i <data.Value.Count; i++)
                {
                    // find number module in order
                    numberModuleNext = FindModule(data.Value[i], singleSimplModules);
                    
                    if (i == 0)
                    {
                        cuurrentNode = graph.Roots[numberModuleNext];
                       orderModules = numberModuleNext;
                        continue;
                    }

                    // if connection to the same module or this module is have next children -> continue
                    if (cuurrentNode == graph.Roots[numberModuleNext] || cuurrentNode.Children.Contains(graph.Roots[numberModuleNext]))
                    {
                        cuurrentNode = graph.Roots[numberModuleNext];
                        orderModules = numberModuleNext;
                        continue;
                    }

                    // if next module finded in number of order that less than this module -> reverse connection
                    if (orderModules > numberModuleNext)
                    {
                        amountFeedbackConnections++;
                    }

                    graph.AddChildrens(cuurrentNode.Value, graph.Roots[numberModuleNext]);
                    // shift to next module
                    cuurrentNode = graph.Roots[numberModuleNext];
                    orderModules = numberModuleNext;
                }
                orderModules = 0;
            }

            return amountFeedbackConnections;
        }

        /// <summary>
        /// Find module for given element 
        /// </summary>
        /// <param name="value">Given value</param>
        /// <param name="singleModule">>Dictionary simplify modules that devision to single modules</param>
        /// <returns>Position module</returns>
        private static int FindModule(string value, Dictionary<int, List<string>> singleModule)
        {
            int numberFindModule = 0;

            for(int i = 0; i < singleModule.Count; i++)
            {
                // if module contains value -> return 
                if(singleModule[i].Contains(value))
                {
                    numberFindModule = i;
                }
            }

            return numberFindModule;
       }

        private static List<Dictionary<int, List<string>>> FactorialElementInArray(List<string> singleModule)
        {
            List<Dictionary<int, List<string>>> listCombModules = new List<Dictionary<int, List<string>>>();

            used = new bool[singleModule.Count];
            //used.Fill(false);
            combinations = new List<List<string>>(); 
            List<string> c = new List<string>();
            GetComb(singleModule, 0, c);

            foreach (var item in combinations)
            {
                listCombModules.Add(ConvertString(item));
            }

            return listCombModules;

        }

        public static void GetComb(List<string> arr, int colindex, List<string> c)
        {

            if (colindex >= arr.Count-2)
            {

                combinations.Add(new List<string>(c));
                return;
            }
            for (int i = 1; i < arr.Count-1; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    if(c.Count == 0)
                    {
                        c.Add(arr[0]);
                    }
                    c.Add(arr[i]);

                    if (c.Count == arr.Count - 1)
                    {
                        c.Add(arr[arr.Count - 1]);
                    }

                    GetComb(arr, colindex + 1, c);

                    if(c.Count == arr.Count)
                    { 
                        c.RemoveAt(c.Count - 1);
                    }

                    c.RemoveAt(c.Count - 1);
                    used[i] = false;
                }
            }
        }

        public static List<string> FindOptimalStructure(List<string> simplifyModules, Dictionary<int, List<string>> singleOperation)
        {
            List<Dictionary<int, List<string>>> listCombModules = new List<Dictionary<int, List<string>>>();

            List<string> optimalPosition = new List<string>();

            listCombModules = FactorialElementInArray(simplifyModules);

            int saveCounterI = 0;

            int amountReverceConnection = 20;

            int tempamountReverceConnection = 20;

            for (int i = 0; i < listCombModules.Count; i++)
            {
                var graph = GraphManager.Create(new List<int> { 0 }, new List<List<string>> { combinations[i] });
                // remove all childrens and parents in node
                graph.Roots.ForEach(x => x.Children.Clear());
                graph.Roots.ForEach(x => x.Parents.Clear());

                // create finish structure and counting the number of reverse
                tempamountReverceConnection = CreateFinishStructure(graph, singleOperation, listCombModules[i]);
                if(tempamountReverceConnection < amountReverceConnection)
                {
                    saveCounterI = i;
                    amountReverceConnection = tempamountReverceConnection;
                }
            }

            optimalPosition = combinations[saveCounterI];
            optimalPosition.Add(amountReverceConnection.ToString());

            return optimalPosition;
        }
    }
}


