using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GKSLab.Bussiness.Logic.Groups_Manager
{
    public static class  RedistributionGroupsManager
    {
        public static List<List<int>> RedistributionGroups(List<List<string>> primaryDate, List<List<int>> groups, List<HashSet<string>> gropsWithStringElement)
        {
            List<List<int>> redistributionGroups = new List<List<int>>();

           return new List<List<int>>();
        }

        public static List<HashSet<string>> CreateGroupsWithStringElement(List<List<string>> StringElement ,List<List<int>> createdGroups)
        {
            List<HashSet<string>> result = new List<HashSet<string>>();
            HashSet<string> temp;

            for (int i = 0; i < createdGroups.Count(); i++ )
            {
                temp = new HashSet<string>();
                foreach(var stringElement in createdGroups[i])
                {
                    StringElement[stringElement-1].ForEach(elem => temp.Add(elem));    
                }
                result.Add(temp);
            }
                return result;
        }
    }
}
