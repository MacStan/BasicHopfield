using System.Collections.Generic;

namespace TspHopfield.Containers
{
    public class FstCity
    {
        public List<Position> positions { get; set; } = new List<Position>();

        public FstCity()
        {
        }

        public FstCity(List<Position> positions)
        {
            this.positions = positions;
        }
    }
}