using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KidsTV8
{
    // Marc C, March PS, 1, PNPSFK3, ANPSFK5
    class Program
    {
        static void TestTestTest()
        {
            var ram = new Ram();
            var corrupter = new RamCorrupter();
            corrupter.Corrupt(ram);
            Console.WriteLine("total faults count: {0}", ram.AllFaultsCount);
            Console.WriteLine();

            Console.WriteLine("rows: {0}, columns: {1}", ram.Rows, ram.Columns);
            var tester = new RamTester(ram);
            Console.WriteLine("march C test:");
            tester.TestMarchC();
            Console.WriteLine("march PS test:");
            tester.TestMarchPS();
            Console.WriteLine(ram);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                TestTestTest();
                if (Console.ReadLine() == "exit") break;
            }
        }
    }
}
