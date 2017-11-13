using System.Collections.Generic;

namespace TspHopfield.Containers
{
    public class Position
    {
        public List<SecCity> Cities { get; set; } = new List<SecCity>();

        public Position()
        {
        }

        public Position(List<SecCity> cities)
        {
            this.Cities = cities;
        }
    }
}