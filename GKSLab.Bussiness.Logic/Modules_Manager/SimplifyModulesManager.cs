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
    }
}
