using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKSLab.Bussiness.Entities
{
    public class ComparationResult
    {
        public ComparationResult(int amount, int countOfUniqueElements)
        {
            ResultingMatrix = new List<List<int>>(countOfUniqueElements);
            for (int i = 0; i < amount; i++)
            {
                ResultingMatrix.Add(new List<int>(countOfUniqueElements));
            }
            for (int i = 0; i < amount; i++)
            {
                for (var j = 0; j < amount; j++)
                {
                    ResultingMatrix[i].Add(0);
                }
            }
        }
        
        public int? UniqueElementsAmount { get; set; }
        public List<List<int>> ResultingMatrix { get; set; }
    }
}
