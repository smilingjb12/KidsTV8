using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsTV8
{
    public class CellWriteEventArgs : EventArgs
    {
        public CellWriteEventArgs(int previousValue, int newValue)
        {
            PreviousValue = previousValue;
            NewValue = newValue;
        }

        public int PreviousValue { get; private set; }
        public int NewValue { get; private set; }
    }
}
