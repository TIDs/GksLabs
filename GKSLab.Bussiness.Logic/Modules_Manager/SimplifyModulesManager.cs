using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKSLab.Bussiness.Logic.Modules_Manager
{
    public static class SimplifyModulesManager
    {
        public static List<HashSet<string>> SimplifyModules(List<HashSet<string>> primaryModules)
        {
            List<HashSet<string>> unionModules = new List<HashSet<string>>();
            List<HashSet<string>> result = new List<HashSet<string>>();
            HashSet<string> temp;
            HashSet<string> repetedElement = new HashSet<string>();
            //unionModules = UnionModulesToOneList(primaryModules);

            //primaryModules.Sort(delegate(HashSet<string> a, HashSet<string> b)
            //{
            //    if (a.Count > b.Count) return 1;
            //    else return -1;
            //});

            for (int i = 0; i < primaryModules.Count; i++ )
            {
                for (int j = i+1; j < primaryModules.Count; j++)
                {
                    if(primaryModules[i].Count > primaryModules[j].Count)
                    {
                        var temps = primaryModules[i];
                        primaryModules[i] = primaryModules[j];
                        primaryModules[j] = temps;
                    }
                }
            }

                for (int i = 0; i < primaryModules.Count; i++)
                {
                    temp = new HashSet<string>();
                    temp = DeleteModules(primaryModules[i], i + 1, primaryModules, repetedElement);
                    if (temp == null) continue;
                    else result.Add(temp);
                }

            return result;
        }

        public static List<HashSet<string>> UnionModulesToOneList(List<List<List<string>>> ModulesToUnion)
        {
            List<HashSet<string>> result = new List<HashSet<string>>();
            HashSet<string> temp;

            for (int i = 0; i < ModulesToUnion.Count; i++)
            {
                for (int j = 0; j < ModulesToUnion[i].Count; j++ )
                {
                    temp = new HashSet<string>();
                    ModulesToUnion[i][j].ForEach(x => temp.Add(x));
                    result.Add(temp);
                }
            }

            return result;
        }

        public static HashSet<string> DeleteModules(HashSet<string> fixedElements, int counter, List<HashSet<string>> data, HashSet<string> repetElem)
        {
            HashSet<string> checkModules = new HashSet<string>(fixedElements.Except(repetElem));
            int amountElement = checkModules.Count;

            for (int j = counter; j < data.Count; j++ )
            {
                if (checkModules.Except(data[j]).Count() == 0) return null;
                else if(checkModules.Except(data[j]).Count() != amountElement && data[j].Count > checkModules.Count)
                {
                    checkModules.Except(fixedElements);
                    foreach(var item in checkModules)
                    {
                        repetElem.Add(item);
                    }

                    checkModules = fixedElements;
                }
            }
            return checkModules;
        }


        /// <summary>
        /// Find first module in finish structure
        /// </summary>
        /// <param name="dictModule"> Dictionary devision single module</param>
        /// <param name="simplifModule">Simplify modules</param>
        /// <param name="element">Element in first module</param>
        private static void FindFirstModule(Dictionary<int, List<string>> dictModule, List<string> simplifModule, string element)
        {
            for (int i = 0; i < dictModule.Count; i++)
            {
                // find modules that contains first element and shift him in first position
                if (dictModule[i].Contains(element) && i != 0)
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

        public static void FindOrderModules(Dictionary<int, List<string>> simpModule, Dictionary<int, List<string>> simplInputData, List<string> simplifyModules)
        {
            // first and last elemеnts which in most primaryData in first and last positions 
            List<string> firstAndLastElements = new List<string>();

            firstAndLastElements = FindFirstAndLastElements(simplInputData);

            // find first and last module in finish structured and shift him to accordyngly first and last position
            FindFirstModule(simpModule, simplifyModules, firstAndLastElements[0]);
            FindLastModule(simpModule, simplifyModules, firstAndLastElements[1]);
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
                // find modules that contains last element and shift him in last position
                if (dictModule[i].Contains(element) && i != amountElement)
                {
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
        private static List<string> FindFirstAndLastElements(Dictionary<int, List<string>> prData)
        {
            List<string> result = new List<string>();
            List<string> listFirstElemnts = new List<string>();
            List<string> listLastElements = new List<string>();

            foreach (var dict in prData)
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
        private static string FindGivenElement(List<string> listForFind)
        {
            string element;
            string findedElement = "";
            int amountOneElementInList = 0;

            for (int i = 0; i < listForFind.Count; i++)
            {
                element = listForFind[i];
                if (i == 0)
                {
                    // choose the first element as most frequent
                    amountOneElementInList = listForFind.Count(x => x == element);
                    findedElement = element;
                }
                // check if exist element that is more frequent than previosely
                else if (amountOneElementInList < listForFind.Count(x => x == element))
                {
                    amountOneElementInList = listForFind.Count(x => x == element);
                    findedElement = element;
                }
            }

            return findedElement;
        }

        public static void FindOrderModules(Dictionary<int, List<string>> simpModule, Dictionary<int, List<string>> simplInputData, List<HashSet<string>> simplifyModules)
        {
            throw new NotImplementedException();
        }
    }
}
