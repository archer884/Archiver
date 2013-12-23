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
            Assert.IsTrue(ConfigurationService.Configuration.ArchiveJobs.Count > 0);
        }

        [TestMethod]
        public void test_002_FilterConfiguration()
        {
            Assert.IsTrue(ConfigurationService.Configuration.ArchiveJobs
                .All(job => 
                    job.Filter == null 
                    || !string.IsNullOrWhiteSpace(job.Filter)));
        }
    }
}
