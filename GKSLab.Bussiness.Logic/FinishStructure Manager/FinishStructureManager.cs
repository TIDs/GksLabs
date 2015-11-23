using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GKSLab.Bussiness.Logic.FinishStructure_Manager
{
    public static class FinishStructureManager
    {
        /// <summary>
        /// Create finish structure automatic system
        /// </summary>
        /// <param name="simplifyModules">Simplify modules</param>
        /// <param name="primaryData">Primary data</param>
        public static void CreateFinishStructure(List<string> simplifyModules, List<string> primaryData)
        {
            // dictionary with single string element
            Dictionary<int, List<string>> singleOperation = new Dictionary<int, List<string>>();

            //dictionary with single string modules
            Dictionary<int, List<string>> singleModule = new Dictionary<int, List<string>>();

            // first and last elemеnts which in most primaryData in first and last positions 
            List<string> firstAndLastElements = new List<string>();

            singleOperation = ConvertString(primaryData);
            singleModule = ConvertString(simplifyModules);

            firstAndLastElements = FindFirstAndLastElements(singleOperation);

            FindFirstModule(singleModule, simplifyModules, firstAndLastElements[0]);
            FindLastModule(singleModule,simplifyModules, firstAndLastElements[1]);

            
        }

        /// <summary>
        /// Find first module in finish structure
        /// </summary>
        /// <param name="dictModule"> Dictionary devision single module</param>
        /// <param name="simplifModule">Simplify modules</param>
        /// <param name="element">Element in first module</param>
        private static void FindFirstModule(Dictionary<int, List<string>> dictModule, List<string> simplifModule,  string element)
        {
            for (int i = 0; i < dictModule.Count; i++)
            {
                if (dictModule[i].Contains(element) && i != 0)
                {
                    var tempDictionary = dictModule[0];
                    dictModule[0] = dictModule[i];
                    dictModule[i] = tempDictionary;

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
        private static void FindLastModule(Dictionary<int, List<string>> dictModule, List<string> simplifModule, string element)
        {
            int amountElement = dictModule.Count - 1;

            for (int i = 0; i < dictModule.Count; i++)
            {
                if (dictModule[i].Contains(element) && i != amountElement)
                {
                    var tempDictionary = dictModule[amountElement];
                    dictModule[amountElement] = dictModule[i];
                    dictModule[i] = tempDictionary;

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
        private static List<string> FindFirstAndLastElements(Dictionary<int, List<string>> prData)
        {
            List<string> result = new List<string>();
            List<string> listFirstElemnts = new List<string>();
            List<string> listLastElements = new List<string>();

            foreach( var dict in prData)
            {
                var amount = dict.Value.Count;
                listFirstElemnts.Add(dict.Value[0]);
                listLastElements.Add(dict.Value[amount - 1]);
            }

            result.Add(FindSetElement(listFirstElemnts));
            result.Add(FindSetElement(listLastElements));

            return result;
        }

        private static string FindSetElement(List<string> listForFind)
        {
            string element;
            string findedElement = "";
            int amountOneElementInList = 0;

            for (int i = 0; i < listForFind.Count; i++)
            {
                element = listForFind[i];
                if (i == 0)
                {
                    amountOneElementInList = listForFind.Count(x => x == element);
                    findedElement = element;
                }
                else if (amountOneElementInList < listForFind.Count(x => x == element))
                {
                    amountOneElementInList = listForFind.Count(x => x == element);
                    findedElement = element;
                }
            }

            return findedElement;
        }

        /// <summary>
        /// Convert string with a lot of element to string with consits is array with one element
        /// </summary>
        /// <param name="prData">Primary data</param>
        private static Dictionary<int, List<string>> ConvertString(List<string> prData)
        {
            List<string> devisionOperations;

            // devision string by pattern (string element -> some element that dont equal null)
            string pattern = @"(\w)(\S)";

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
    }
}
