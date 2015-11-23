using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GKSLab.Bussiness.Entities;

namespace GKSLab.Models.ViewModels
{
    public class GraphLabViewModel
    {
        public List<List<int>> ResultingMatrix { get; set; }
        public List<List<string>> InputData { get; set; }
        public int Unique { get; set; }
        public List<List<int>> Groups { get; set; }
        public List<HashSet<string>> GroupString { get; set; }
        public List<List<int>> RedistributedGroups { get; set; }

        public List<List<List<string>>> groupsWithModules = new List<List<List<string>>>();
    }
}