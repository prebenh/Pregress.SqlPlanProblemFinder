using System;
using System.Collections.Generic;

namespace Pregress.SqlPlanProblemFinder
{
    internal class ConsolePrinter
    {
        private readonly IEnumerable<IndexSuggestion> _indexScans;
        private IEnumerable<IndexSuggestion> _tableScans;

        public ConsolePrinter(IEnumerable<IndexSuggestion> indexScans, IEnumerable<IndexSuggestion> tableScans)
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

        private static void PrintScan(IndexSuggestion suggestion)
        {
            Console.WriteLine(suggestion);
            Console.WriteLine(suggestion.CreateStatement);
            Console.WriteLine();
        }
    }
}