using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using _02___TypedDataSet;
using System;
using System.IO;

namespace _04___Testing
{
    
    
    /// <summary>
    ///This is a test class for MetricsTest and is intended
    ///to contain all MetricsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MetricsTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CalcExpectedProfit
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("..\\03 - Get Disconnected", "/03 - Get Disconnected")]
        [UrlToTest("http://localhost/03 - Get Disconnected/03_Testing.aspx")]
        public void CalcExpectedProfitTest() {
            Decimal actual = Metrics_Accessor.CalcExpectedProfit(10M, 20M, 5M);
            Assert.AreEqual(5M, actual);
        }

        /// <summary>
        ///A test for AddExpectedProfit
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("..\\03 - Get Disconnected", "/03 - Get Disconnected")]
        [UrlToTest("http://localhost/03 - Get Disconnected/03_Testing.aspx")]
        [DeploymentItem("TestData.xml")]
        public void AddExpectedProfitTest() {
            AdventureWorksDS ds = new AdventureWorksDS();
            ds.ReadXml("TestData.xml");
            Metrics_Accessor.AddExpectedProfit(ds.Product);
            Assert.IsNotNull(ds.Product.Columns["ExpectedProfit"]);
            Assert.AreEqual(ds.Product.Compute("sum(ExpectedProfit)", String.Empty), 6712.2501M);
        }
    }
}
