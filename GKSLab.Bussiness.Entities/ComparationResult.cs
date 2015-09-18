using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKSLab.Bussiness.Entities
{
    public class ComparationResult
    {
        public ComparationResult()
        {
            ResultingMatrix = new List<List<int>>();
        }

        public int? UniqueElementsAmount { get; set; }
        public List<List<int>> ResultingMatrix { get; set; }
    }
}
