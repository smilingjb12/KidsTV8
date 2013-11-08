using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KidsTV8
{
    public class ActiveSlaveCell : Cell
    {
        public List<Cell> Neighbours { get; set; }
        public List<PatternType> Pattern { get; set; }
        public TransitionType Transition { get; set; }

        public ActiveSlaveCell()
        {

        }

        public void MasterCellValueChangedHandler(object sender, CellWriteEventArgs e)
        {
            if (ComputePattern().SequenceEqual(this.Pattern))
            {
                this.value = GetValueFromTransition();
            }
        }

        private int GetValueFromTransition()
        {
            switch (Transition)
            {
                case TransitionType.ToOne: 
                    return 1;
                case TransitionType.ToZero:
                    return 0;
                case TransitionType.Invert:
                    return this.value.Invert();
            }
            return -1;
        }

        private IEnumerable<PatternType> ComputePattern()
        {
            return Neighbours
                .Select(c => c.Read() == 0 ? PatternType.ConstZero : PatternType.ConstOne);
        }

        public override string ToString()
        {
            return "S";
        }
    }
}
