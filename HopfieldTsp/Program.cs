using HopfieldTsp;
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
            var size = 10;
            var xx = new double[,] {
                {0.4000, 0.4439},
                {0.2439, 0.1463},
                {0.1707, 0.2293},
                {0.2293, 0.7610},
                {0.5171, 0.9414},
                {0.8732, 0.6536},
                {0.6878, 0.5219},
                {0.8488, 0.3609},
                {0.6683, 0.2536},
                {0.6195, 0.2634}};
            var patterns = DistancesCreator.CreateDistances(xx);
            /* { 0, 2, 3.6,3.6},
            { 2, 0, 2.33,3 },
            { 3.6,2.33,0,1.41 },
            { 3.6, 3, 1.41,0},*/

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