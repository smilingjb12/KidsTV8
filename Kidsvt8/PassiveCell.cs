using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsTV8
{
    public class PassiveCell : Cell
    {
        public const int NeighboursCount = 2;
        private readonly List<Cell> neighbours;
        private readonly int[] pattern;

        public PassiveCell(IEnumerable<Cell> neighbours, IEnumerable<int> pattern)
        {
            if (neighbours.Count() != NeighboursCount) throw new ArgumentException("neighbours");
            if (pattern.Count() != NeighboursCount) throw new ArgumentException("pattern");

            this.neighbours = neighbours.ToList();
            this.pattern = pattern.ToArray();
        }

        public override void Write(int value)
        {
            if (ComputePattern().SequenceEqual(this.pattern))
            {
                return; // don't write
            }
            this.value = value;
        }

        public IEnumerable<int> ComputePattern()
        {
            return neighbours.Select(n => n.Read());
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
