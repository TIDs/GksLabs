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

            // redistributionGroups
             for (int i = 0; i < GropsWithStringElement.Count; i++)
            {
                 test.Add(OverlappingGroups(uniqueElements, i + 1, primaryDate, groups[i], GropsWithStringElement[i], GropsWithStringElement, groups));
                if ( test[i].Count < 1)
                {
                    continue;
                }
                else
                {
                    redistributionGroups.Add(test[i]);
                }
                 
            }
              
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


        public static List<int> OverlappingGroups(HashSet<int> uniqueElement, int counter, List<List<string>> inputDate, List<int> fixedGroupWithInt, HashSet<string> fixedGroupWithString, List<HashSet<string>> groupsWithString, List<List<int>> groupsWithInt)
        {
            List<int> result = new List<int>();
            HashSet<string> temp;

            for (int i = 0; i < fixedGroupWithInt.Count; i++)
            {
                if (uniqueElement.Contains(fixedGroupWithInt[i]))
                {
                    continue;
                }
                else
                {
                    result.Add(fixedGroupWithInt[i]);
                    uniqueElement.Add(fixedGroupWithInt[i]);
                }
            }

            if (result.Count == 0)
            {
                return null;
            }

            for (int j = counter; j < groupsWithString.Count; j++)
            {
                temp = new HashSet<string>(groupsWithString[j]);
                temp.ExceptWith(fixedGroupWithString);
                if (temp.Count == 0)
                {
                    foreach (var i in groupsWithInt[j])
                    {
                        result.Add(i);
                        uniqueElement.Add(i);
                    }
                }
                else
                {
                    if (groupsWithInt[j].Count > 1)
                        for (int k = 0; k < groupsWithInt[j].Count; k++)
                        {
                            var newElementInGroup = OverlappingElement(inputDate, fixedGroupWithString, groupsWithString[j], groupsWithInt[j][k]);
                            if (newElementInGroup != 0)
                            {
                                result.Add(newElementInGroup);
                                uniqueElement.Add(newElementInGroup);
                            }
                        }
                }
            }
            return result;
        }

        public static int OverlappingElement(List<List<string>> inputDate, HashSet<string> fixedGroupString, HashSet<string> checkedGroupString, int checkedIntElement)
        {
            HashSet<string> checkedStringElement;
            int result = 0;
            checkedStringElement = new HashSet<string>(inputDate[checkedIntElement - 1]);
            checkedStringElement.ExceptWith(fixedGroupString);
            if (checkedStringElement.Count == 0)
            {
                result = checkedIntElement;
            }

            return result;
        }

    }
}
 

