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
                var rnd = new Random();
                var size = 3;
                var patterns = new double[,]
                     {
                     { 0, 2, 4},
                     { 2, 0, 1.41},
                     { 4, 1.41, 0},
                         /* { 0, 2, 3.6,3.6},
                         { 2, 0, 2.33,3 },
                         { 3.6,2.33,0,1.41 },
                         { 3.6, 3, 1.41,0},*/
                    };
                var net = new Network(size, patterns);
                net.CreateWeights();

                for (int i = 0; i < 10000; i++)
                {
                    net.PrintState();
                    net.Update2();
                    //net.PrintWeights();
                }
                Thread.Sleep(60 * 1000);
            }
        }
    }
}