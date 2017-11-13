using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TspHopfield;

namespace UnitTests

{
    internal class TspHopfieldUT
    {
        [TestClass]
        public class UnitTest1
        {
            [TestMethod]
            public void TestMethod1()
            {
                var patterns = new List<List<double>>()
                 {
                     new List<double>(){ 0, 2, 4},
                     new List<double>(){ 2, 0, 2.0 * Math.Sqrt(2) },
                     new List<double>(){ 4, 2.0 * Math.Sqrt(2), 0 },
                };
                var net = new Network(3, patterns);
                net.CreateWeights();

                Console.Write("test");
            }
        }
    }
}