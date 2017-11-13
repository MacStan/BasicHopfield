using System;
using System.Collections.Generic;
using System.Text;

namespace TspHopfield.Containers
{
    public class WeightsContainer
    {
        public List<FstCity> cities { get; set; } = new List<FstCity>();

        public WeightsContainer()
        {
        }

        public WeightsContainer(List<FstCity> cities)
        {
            this.cities = cities;
        }
    }
}