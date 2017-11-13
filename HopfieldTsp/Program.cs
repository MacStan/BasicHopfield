using System;
using System.Collections.Generic;
using System.Threading;

namespace TspHopfield
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rnd = new Random();
            var size = 4;
            var patterns = new double[,]
                 {   { 0, 2, 3.6,3.6},
                     { 2, 0, 2.33,3 },
                     { 3.6,2.33,0,1.41 },
                     { 3.6, 3, 1.41,0},
                };
            var net = new Network(size, patterns);
            net.CreateWeights();

            for (int i = 0; i < 10000; i++)
            {
                net.Update();
                net.PrintState();
            }
            Thread.Sleep(60 * 1000);
        }
    }
}