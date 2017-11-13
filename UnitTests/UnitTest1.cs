using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicHopfield;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = new Network(5);
            var patterns = new List<List<int>>()
             {
                 new List<int>(){1,0,1,0,1 },
                 new List<int>(){ 0,1,1,0,1 }
            };
            x.CalculateWeights(patterns);
            Assert.AreEqual(x.weights[0].weights[0], 0);
            Assert.AreEqual(x.weights[0].weights[1], -2);
            Assert.AreEqual(x.weights[0].weights[2], 0);
            Assert.AreEqual(x.weights[0].weights[3], 0);
            Assert.AreEqual(x.weights[0].weights[4], 0);

            Assert.AreEqual(x.weights[1].weights[0], -2);
            Assert.AreEqual(x.weights[1].weights[1], 0);
            Assert.AreEqual(x.weights[1].weights[2], 0);
            Assert.AreEqual(x.weights[1].weights[3], 0);
            Assert.AreEqual(x.weights[1].weights[4], 0);

            Assert.AreEqual(x.weights[2].weights[0], 0);
            Assert.AreEqual(x.weights[2].weights[1], 0);
            Assert.AreEqual(x.weights[2].weights[2], 0);
            Assert.AreEqual(x.weights[2].weights[3], -2);
            Assert.AreEqual(x.weights[2].weights[4], 2);

            Assert.AreEqual(x.weights[3].weights[0], 0);
            Assert.AreEqual(x.weights[3].weights[1], 0);
            Assert.AreEqual(x.weights[3].weights[2], -2);
            Assert.AreEqual(x.weights[3].weights[3], 0);
            Assert.AreEqual(x.weights[3].weights[4], -2);

            Assert.AreEqual(x.weights[4].weights[0], 0);
            Assert.AreEqual(x.weights[4].weights[1], 0);
            Assert.AreEqual(x.weights[4].weights[2], 2);
            Assert.AreEqual(x.weights[4].weights[3], -2);
            Assert.AreEqual(x.weights[4].weights[4], 0);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int size = 5;
            var x = new Network(size);
            var patterns = new List<List<int>>()
             {
                 new List<int>(){1,0,1,0,1 },
                 new List<int>(){ 0,1,1,0,1 }
            };
            x.CalculateWeights(patterns);
            Random rnd = new Random();
            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    var node = rnd.Next(0, size - 1);
                    x.UpdateNode(node);
                }
                if (x.IsStable())
                {
                    break;
                }
            }
            Console.WriteLine($"{x.nodes[0]}, {x.nodes[1]}, {x.nodes[2]},{x.nodes[3]},{x.nodes[4]} ");
            if (x.nodes[0] == 0
                && x.nodes[1] == 1
                && x.nodes[2] == 1
                && x.nodes[3] == 0
                && x.nodes[4] == 1)
            {
                Console.WriteLine("Match with 01101 - random dependancy");
                return;
            }
            if (x.nodes[0] == 1
                && x.nodes[1] == 0
                && x.nodes[2] == 1
                && x.nodes[3] == 0
                && x.nodes[4] == 1)
            {
                Console.WriteLine("Match with 10101 - random dependancy");
                return;
            }
            Assert.Fail("Not matched any answers :(");
        }
    }
}