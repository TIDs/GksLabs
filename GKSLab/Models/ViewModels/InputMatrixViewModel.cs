using System;
using System.Collections.Generic;

namespace GKSLab.Models.ViewModels
{
    public class InputMatrixViewModel
    {
        public List<RowItem> MatrixList { get; set; }
    }

    public class RowItem
    {
        public List<String> Row { get; set; }
    } 
}