﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GKSLab.Models.ViewModels
{
    public class СreationGraphModel
    {
        public IEnumerable<List<int>> Groups { get; set; }
        public List<List<string>> InputData { get; set; }
        public List<List<int>> RedistributedGroups { get; set; }
        public List<List<List<string>>> groupsWithModulesAllModulesForOutput { get; set; }
        public List<HashSet<string>> moduleToSimpl { get; set; }
        public Dictionary<int, List<string>> SimplInputDataDictionary { get; set; }
        public Dictionary<int, List<string>> ModuleSimplDictionary { get; set; }

        public List<string> ForOutputInGraph { get; set; }
    }
}