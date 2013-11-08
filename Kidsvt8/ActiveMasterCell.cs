using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsTV8
{
    public class ActiveMasterCell : Cell
    {
        public const int NeighboursCount = 4;

        private readonly Cell slave;

        public event EventHandler<CellWriteEventArgs> ValueChanged;

        public ActiveMasterCell(Cell slave)
        {
            this.slave = slave;
        }

        public override void Write(int value)
        {
            if (this.value != value)
            {
                ValueChanged(sender: this, e: new CellWriteEventArgs(this.value, value));
                this.value = value;
            }       
        }

        public override string ToString()
        {
            return "M";
        }
    }
}
