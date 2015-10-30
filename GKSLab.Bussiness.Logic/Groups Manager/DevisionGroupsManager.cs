using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GKSLab.Bussiness.Entities;

namespace GKSLab.Bussiness.Logic.Groups_Manager
{
    public static class DevisionGroupsManager
    {
        public static List<List<int>> CreateGroups(ComparationResult dataForCreateGroups)
        {
            var elementInGroup = new HashSet<int>();
            var uniqueElement = new HashSet<int>();
            var maxElement = 0;
            var size = dataForCreateGroups.ResultingMatrix.Count();
            List<List<int>> groups = new List<List<int>>();
            List<int> position = new List<int>();

            for (int i = 0; i < size; i++)
            {
                if (maxElement < dataForCreateGroups.ResultingMatrix[i].Max())
                {
                    maxElement = dataForCreateGroups.ResultingMatrix[i].Max();
                }
            }

            while(elementInGroup.Count() < size)
            {
                position = new List<int>();

                if (size - elementInGroup.Count() < 3)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (!elementInGroup.Contains(i+1))
                        {
                            position.Add(i+1);
                        }
                    }
                    groups.Add(position);
                    break;
                }

                uniqueElement.Clear();

                for (int i = 0; i < size; i++ )
                {
                    for(int j = 0; j < size; j++)
                    {
                        if(dataForCreateGroups.ResultingMatrix[i][j] == maxElement)
                        {
                            if(!elementInGroup.Contains(i+1) && !elementInGroup.Contains(j+1) && position.Count() == 0)
                            {
                                position.Add(i + 1);
                                position.Add(j + 1);
                                elementInGroup.Add(i+1);
                                elementInGroup.Add(j+1);
                                uniqueElement.Add(i + 1);
                                uniqueElement.Add(j + 1);
                            }
                            else if (uniqueElement.Contains(i + 1) && !uniqueElement.Contains(j + 1)&& !elementInGroup.Contains(j+1) && position.Count() > 0)
                            {
                                position.Add(j + 1);
                                elementInGroup.Add(j+1);
                            }
                            else if (!uniqueElement.Contains(i + 1) && uniqueElement.Contains(j + 1) && !elementInGroup.Contains(i+1) && position.Count() > 0)
                            {
                                position.Add(i + 1);
                                elementInGroup.Add(i+1);
                            }
                        }
                    }
                }

                if (position.Count() == 0)
                    maxElement -= 1;
                else
                    groups.Add(position);

            }
                return groups;
        }
    }
}
