using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GKSLab.Bussiness.Logic.FinishStructure_Manager;

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
            List<HashSet<string>> newPrimaryModules = new List<HashSet<string>>();

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

            primaryModules.ForEach(x => newPrimaryModules.Add(x));

            for (int i = 0; i < newPrimaryModules.Count; i++)
                {
                    temp = new HashSet<string>();
                    temp = DeleteModules(newPrimaryModules[i], i + 1, newPrimaryModules, repetedElement);
                    if (temp == null) continue;
                    else result.Add(temp);
                }

            return result;
        }

        public static HashSet<string> DeleteModules(HashSet<string> fixedElements, int counter, List<HashSet<string>> data, HashSet<string> repetElem)
        {
            HashSet<string> checkModules = new HashSet<string>(fixedElements.Except(repetElem));
            int amountElement = checkModules.Count;
            HashSet<string> newModules;

            for (int j = counter; j < data.Count; j++ )
            {
                if (checkModules.Except(data[j].Except(repetElem)).Count() == 0) return null;
                else if(checkModules.Except(data[j]).Count() != amountElement)
                {
                    if(data[j].Except(repetElem).Count() >= checkModules.Count)
                    {
                          checkModules.Except(fixedElements);
                          foreach (var item in checkModules)
                          {
                               repetElem.Add(item);
                          }
                          break;
                    } 
                    else
                    {
                        newModules = new HashSet<string>();
                         foreach (var item in checkModules.Except(data[j].Except(repetElem)))
                         {
                             newModules.Add(item);
                         }

                        checkModules = newModules;
                    }
                }
            }
            return checkModules;
        }

    }
}
