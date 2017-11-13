using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHopfield
{
    public class Network
    {
        private int size = 0;

        public List<HorizontalWeights> weights = new List<HorizontalWeights>();

        public List<int> nodes = new List<int>();

        public Network(int size)
        {
            this.size = size;
            nodes = new List<int>() { 1, 1, 1, 1, 1 };
        }

        public void CalculateWeights(List<List<int>> patterns)
        {
            for (int y = 0; y < size; y++)
            {
                weights.Add(new HorizontalWeights(size, y, patterns));
            }
        }

        public void UpdateNode(int node_x)
        {
            var activation = 0.0;
            for (int x1 = 0; x1 < size; x1++)
            {
                if (x1 != node_x)
                {
                    activation += weights[node_x].weights[x1] * nodes[x1];
                }
            }

            nodes[node_x] = activation >= 0 ? 1 : 0;
        }

        public bool IsStable()
        {
            for (int i = 0; i < size; i++)
            {
                var temp = nodes[i];
                UpdateNode(i);
                if (nodes[i] != temp)
                {
                    return false;
                }
            }
            return true;
        }
    }
}