using Microsoft.VisualStudio.TestTools.UnitTesting;
using TacViewKillReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacViewKillReader.Tests
{
    [TestClass()]
    public class AnalyzerTests
    {
        [TestMethod()]
        public void AnalyzeTest()
        {
            var path = "D:\\Desktop\\Tacview.xml";
            var analyzer = new Analyzer();

            var kills = analyzer.GetKills(path);
            var filteredKills = analyzer.FilterKills(kills);

            var output = new Output();
            output.CreateFile(filteredKills, "D:\\Desktop\\Modern Campaign\\Analyzing\\test.txt");
            //Assert.Fail();
        }

        [TestMethod()]
        public void CombineMissionResults()
        {
            var path = "D:\\Desktop\\Modern Campaign\\Analyzing\\test.txt";
            var analyzer = new Analyzer();

            var result = analyzer.AnalyzeMultipleFiles(new List<string>() {path });
        }
    }
}