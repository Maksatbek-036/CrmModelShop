using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmBlTest
{
    [TestClass]
    public class ShopComputerTests
    {
        [TestMethod]
        public void StartTest()
        {
            var model = new ShopComputerModel();
            model.Start();
            Thread.Sleep(1000);
        }
    }
}
