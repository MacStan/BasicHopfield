using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopfieldTsp
{
    public class DistancesCreator
    {
        public static double[,] CreateDistances(double[,] positions)
        {
            var xa = positions.GetLength(1);
            var ya = positions.GetLength(0);
            var distances = new double[ya, ya];
            var largest = 0.0;
            for (int x = 0; x < ya; x++)
            {
                for (int y = 0; y < ya; y++)
                {
                    distances[x, y] =
                        Math.Sqrt(
                            Math.Pow(positions[x, 0] - positions[y, 0], 2)
                        + Math.Pow(positions[x, 1] - positions[y, 1], 2));
                    largest = distances[x, y] > largest ? distances[x, y] : largest;
                }
            }
            for (int x = 0; x < ya; x++)
            {
                for (int y = 0; y < ya; y++)
                {
                    distances[x, y] /= largest;
                }
            }

            return distances;
        }
    }
}