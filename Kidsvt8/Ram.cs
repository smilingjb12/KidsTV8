using System;
using System.Linq;
using System.Text;

namespace KidsTV8
{
    public class Ram
    {
        public static readonly int Size = 1000; // should be 1 Mbit
        private readonly Cell[] cells;
        private readonly int rows, columns;

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }
        
        private int allFaultsCount = -1;
        public int AllFaultsCount
        {
            get
            {
                if (allFaultsCount != -1) return allFaultsCount;
                int activeCells = cells.Count(c => c is ActiveMasterCell) / ActiveMasterCell.NeighboursCount;
                int passiveCells = cells.Count(c => c is PassiveCell);
                return allFaultsCount = activeCells + passiveCells;
            }
        }

        private static Tuple<int, int> GenerateDimensions()
        {
            int a = (int) Math.Sqrt(Size);
            int b;
            while (true)
            {
                b = a;
                int product;
                while (true)
                {
                    product = a*b;
                    if (product >= Size) break;
                    b += 1;
                }
                if (product == Size) break;
                if (a <= 0) return null;
                a -= 1;
            }
            return Tuple.Create(a, b);
        }

        public Ram()
        {
            cells = ConstructCells();
            var dimensions = GenerateDimensions();
            rows = dimensions.Item1;
            columns = dimensions.Item2;
        }

        private Cell[] ConstructCells()
        {
            return Enumerable.Range(0, Size)
                .Select(i => new Cell())
                .ToArray();
        }

        public Cell[] Cells
        {
            get { return cells; }
        }

        public Cell this[int index]
        {
            get { return cells[index]; }
        }

        public Cell this[int row, int column]
        {
            get
            {
                return cells[columns * row + column];
            }
            set
            {
                cells[columns * row + column] = value;
            }
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    res.AppendFormat("{0}", this[r, c]);
                }
                res.AppendLine();
            }
            return res.ToString();
        }
    }
}