using System.Collections.Generic;

namespace TspHopfield.Containers
{
    public class SecCity
    {
        public List<double> Position { get; set; } = new List<double>();

        public SecCity()
        {
        }

        public SecCity(List<double> position)
        {
            this.Position = position;
        }
    }
}