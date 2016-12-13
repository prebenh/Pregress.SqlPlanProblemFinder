using System;
using System.Collections.Generic;

namespace SqlPlanProblemFinder
{
    internal class ConsolePrinter
    {
        private readonly IEnumerable<IndexScan> _indexScans;
        private IEnumerable<IndexScan> _tableScans;

        public ConsolePrinter(IEnumerable<IndexScan> indexScans, IEnumerable<IndexScan> tableScans)
        {
            _indexScans = indexScans;
            _tableScans = tableScans;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Table scans == Big problems");
            Console.ResetColor();

            foreach (var scan in _tableScans)
            {
                PrintScan(scan);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Index scans == problems");
            Console.WriteLine("Biggest problems are on top");
            Console.ResetColor();

            foreach (var scan in _indexScans)
            {
                PrintScan(scan);
            }

            Console.ResetColor();
        }

        private static void PrintScan(IndexScan scan)
        {
            Console.WriteLine(scan);
            Console.WriteLine(scan.CreateStatement);
            Console.WriteLine();
        }
    }
}