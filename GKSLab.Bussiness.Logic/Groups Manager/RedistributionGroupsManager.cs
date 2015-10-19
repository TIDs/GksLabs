using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GKSLab.Bussiness.Logic.Groups_Manager
{
    public static class  RedistributionGroupsManager
    {
        public static List<List<int>> RedistributionGroups(List<List<string>> primaryDate, List<List<int>> groups, List<HashSet<string>> GropsWithStringElement)
        {
             List<List<int>> redistributionGroups = new List<List<int>>();
             List<List<int>> test = new List<List<int>>();
             HashSet<int> uniqueElements = new HashSet<int>();
 
             SortGroupsWithStringElementAndInt(groups, GropsWithStringElement);
              
             return redistributionGroups;
        }

        public static List<HashSet<string>> CreateGroupsWithStringElement(List<List<string>> StringElement, List<List<int>> createdGroups)
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

        public static void SortGroupsWithStringElementAndInt(List<List<int>> unsortedGroupsWitnInt, List<HashSet<string>> unsortedGroupsWithString)
        {
            for (int i = 0; i < unsortedGroupsWithString.Count; i++)
            {
                var MaxCount = unsortedGroupsWithString[i].Count;

                for (int j = i + 1; j < unsortedGroupsWithString.Count; j++)
                {
                    if (MaxCount < unsortedGroupsWithString[j].Count)
                    {
                        MaxCount = unsortedGroupsWithString[j].Count;
                        unsortedGroupsWithString = wrapString(unsortedGroupsWithString, i, j);
                        unsortedGroupsWitnInt = wrapInt(unsortedGroupsWitnInt, i, j);
                    }
                }
            }
           
        }

        public static List<HashSet<string>> wrapString(List<HashSet<string>> unsortedGroup, int firstList, int MaxList)
        {
            var temp = unsortedGroup[firstList];
            unsortedGroup[firstList] = unsortedGroup[MaxList];
            unsortedGroup[MaxList]= temp;

            return unsortedGroup;
        }

        public static List<List<int>> wrapInt(List<List<int>> unsortedGroup, int firstList, int MaxList)
        {
            var temp = unsortedGroup[firstList];
            unsortedGroup[firstList] = unsortedGroup[MaxList];
            unsortedGroup[MaxList] = temp;

            return unsortedGroup;
        }

    }
}
 