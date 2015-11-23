using System.IO;
using GKSLab.Controllers;
using GKSLab.Tests.MoqContext;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GKSLab.Tests
{
    [TestClass]
    public class Lab5Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new MoqHttpContext();
            var controller = new LabController()
            {
                ControllerContext = new ControllerContext() {HttpContext = context.MockContext.Object}
            };
           // var resultArray = controller.Test(null);
            Assert.IsTrue(true);
        }
    }
}
