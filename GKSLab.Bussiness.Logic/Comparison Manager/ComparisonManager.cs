using System.Collections.Generic;
using System.Linq;
using GKSLab.Bussiness.Entities;

namespace GKSLab.Bussiness.Logic.Comparison_Manager
{
    public static class ComparisonManager
    {
        public static ComparationResult CompareDetails(List<List<string>> inputData)
        {
            var row = new HashSet<string>();
            inputData.ForEach(x => x.ForEach(elem => row.Add(elem)));
            var uniqueElementsAmount = row.Count;
            var result = new ComparationResult(inputData.Count, uniqueElementsAmount)
            {
                UniqueElementsAmount = uniqueElementsAmount
            };
            for (var i = 0; i < inputData.Count; i++)
            {
                for (var j = 0; j < inputData.Count; j++)
                {
                    if (i == j)
                        continue;
                    var comparableDetails = inputData[j];
                    var uniqueElements = inputData[i].Union(inputData[j]).Except(inputData[i].Intersect(inputData[j])).ToList();
                    result.ResultingMatrix[i][j] = (uniqueElementsAmount - uniqueElements.Count);
                }
            }
         
            return result;
        }
    }
}
