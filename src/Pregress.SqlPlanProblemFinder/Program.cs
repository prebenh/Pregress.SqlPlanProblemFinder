using System;
using System.Linq;

namespace Pregress.SqlPlanProblemFinder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Please provide a path to a .sqlplan file");
            }

            var path = args[0].Replace("\"", "").Replace("'", "");
            var document = new SqlPlanLoader().Load(path);

            var tableScans = new FindTableScans(document).Find().ToList();
            var indexScans = new FindIndexScans(document).Find().ToList();

            var printer = new ConsolePrinter(indexScans, tableScans);
            printer.Print();

            var creator = new SqlFileCreator(indexScans, tableScans);
            creator.Create("MissingIndexes.sql");
            Console.WriteLine("Created MissingIndexes.sql");

            Console.ReadLine();
        }
    }
}