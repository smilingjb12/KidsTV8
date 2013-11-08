using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace KidsTV8
{
    public class RamCorrupter
    {
        private static readonly Random random = new Random(DateTime.Now.Millisecond);

        public void Corrupt(Ram ram)
        {
            AddPassiveFaultsTo(ram);
            AddActiveFaultsTo(ram);
        }

        private void AddPassiveFaultsTo(Ram ram)
        {
            for (int i = 0; i < Ram.Size / 10; ++i)
            {
                int row = random.Next(ram.Rows);
                int column = random.Next(ram.Columns);
                if (column - 1 < 0 || column + 1 >= ram.Columns) continue;
                if (ram[row, column - 1] is PassiveCell || ram[row, column + 1] is PassiveCell) continue;
                var passiveCell = new PassiveCell(
                    neighbours: new[] { ram[row, column - 1], ram[row, column + 1]},
                    pattern: Enumerable.Range(0, PassiveCell.NeighboursCount).Select(_ => random.Next(2))
                );
                ram[row, column] = passiveCell;
            }
        }

        private void AddActiveFaultsTo(Ram ram)
        {
            for (int i = 0; i < Ram.Size / 30; ++i)
            {
                int row = random.Next(ram.Rows);
                int column = random.Next(ram.Columns);
                if (column - 1 < 0 || column + 1 >= ram.Columns) continue;
                if (row - 1 < 0 || row + 1 >= ram.Rows) continue;
                var activeCells = new[] { ram[row, column], ram[row - 1, column], ram[row + 1, column], ram[row, column - 1], ram[row, column + 1] };
                if (activeCells.All(cell => cell.GetType() == typeof(Cell)))
                {
                    ActiveSlaveCell slave = new ActiveSlaveCell();
                    ram[row, column] = slave;
                    ram[row - 1, column] = new ActiveMasterCell(slave);
                    ram[row + 1, column] = new ActiveMasterCell(slave);
                    ram[row, column - 1] = new ActiveMasterCell(slave);
                    ram[row, column + 1] = new ActiveMasterCell(slave);
                    slave.Neighbours = new List<Cell>
                    {
                        ram[row - 1, column], ram[row + 1, column],
                        ram[row, column - 1], ram[row, column + 1]
                    };
                    slave.Transition = GenerateRandomTransition();
                    slave.Pattern = GenerateRandomPattern(ActiveMasterCell.NeighboursCount).ToList();
                    slave.Neighbours
                        .Cast<ActiveMasterCell>()
                        .ToList()
                        .ForEach(master => master.ValueChanged += slave.MasterCellValueChangedHandler);
                }
            }
        }

        private TransitionType GenerateRandomTransition()
        {
            var transitions = Enum
                .GetValues(typeof(TransitionType))
                .Cast<TransitionType>()
                .ToList();
            return transitions[random.Next(transitions.Count)];
        }

        private IEnumerable<PatternType> GenerateRandomPattern(int size)
        {
            return Enumerable.Range(0, size)
                .Select(_ => GenerateRandomPatternType());
        }

        private PatternType GenerateRandomPatternType()
        {
            var patternTypes = Enum
                .GetValues(typeof(PatternType))
                .Cast<PatternType>()
                .ToList();
            return patternTypes[random.Next(patternTypes.Count)];
        }
    }
}