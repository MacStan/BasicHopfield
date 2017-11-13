using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHopfield
{
    public class HorizontalWeights
    {
        public List<double> weights = new List<double>();

        public HorizontalWeights(int size, int position_y, List<List<int>> patterns)
        {
            for (int x = 0; x < size; x++)
            {
                double weight = 0;
                if (x != position_y)
                {
                    foreach (var pat in patterns)
                    {
                        weight += (2 * pat[position_y] - 1) * (2 * pat[x] - 1);
                    }
                }
                weights.Add(weight);
            }
        }
    }
}