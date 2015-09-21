using System;
using System.Collections.Generic;
using System.Web;

namespace GKSLab.Models.ViewModels
{
    public class InputMatrixViewModel
    {
        public List<RowItem> MatrixList { get; set; }
        public string Message { get; set; }
        public HttpPostedFileBase PosteFileBase { get; set; }
    }

    public class RowItem
    {
        public List<string> Row { get; set; }
    } 
}