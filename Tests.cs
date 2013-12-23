using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void test_001_ConfigurationHasStuffInIt()
        {
            Assert.IsTrue(ConfigurationService.Configuration.SourceArchivePairs.Count > 0);
        }

        /// <summary>
        /// Obviously this test won't work unless you set the current year/month as the target.
        /// </summary>
        [TestMethod]
        public void test_002_YearMonthCombo()
        {
            Assert.IsTrue(DateTime.Now.ToString("yyyyMM") == "201312");
        }
    }
}
