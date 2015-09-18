using System.Collections.Generic;
using System.Linq;
using GKSLab.Bussiness.Entities;

namespace GKSLab.Bussiness.Logic.Comparison_Manager
{
    public static class ComparisonManager
    {
        public static ComparationResult CompareDetails(List<List<string>> inputData)
        {
            var result = new ComparationResult();
            var row = new List<string>();
            inputData.ForEach(x => x.ForEach(elem => row.Add(elem)));
            var uniqueElementsAmount = inputData.Count;
            result.UniqueElementsAmount = uniqueElementsAmount;
            foreach (var item in inputData)
            {
                row.AddRange(item);
            }
            for (int i = 0; i < inputData.Count; i++)
            {
                for (int j = 0; j < inputData.Count; j++)
                {
                    if (i == j)
                        continue;
                    var comparableDetails = inputData[j];
                    var uniqueElements = inputData[i].Except(inputData[j]).ToList();
                    result.ResultingMatrix[i][j] = (uniqueElementsAmount - uniqueElements.Count);
                }
            }
            return result;
        }
    }
}
