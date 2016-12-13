using System.Collections.Generic;
using System.IO;

namespace Pregress.SqlPlanProblemFinder
{
    internal class SqlFileCreator
    {
        private readonly IEnumerable<IndexScan> _indexScans;
        private IEnumerable<IndexScan> _tableScans;

        public SqlFileCreator(IEnumerable<IndexScan> indexScans, IEnumerable<IndexScan> tableScans)
        {
            _indexScans = indexScans;
            _tableScans = tableScans;
        }

        public void Create(string path)
        {
            using (var writer = File.CreateText(path))
            {
                writer.WriteLine("--Table scans (Big issues)");
                writer.WriteLine("--Create statements");
                foreach (var scan in _tableScans)
                {
                    writer.WriteLine(scan);
                    writer.WriteLine(scan.CreateStatement);
                    writer.WriteLine();
                }

                writer.WriteLine("--Drop statements");
                foreach (var scan in _tableScans)
                {
                    writer.WriteLine(scan);
                    writer.WriteLine(scan.DropStatement);
                    writer.WriteLine();
                }

                writer.WriteLine("--Index scans");
                writer.WriteLine("--Create statements");
                foreach (var scan in _indexScans)
                {
                    writer.WriteLine(scan);
                    writer.WriteLine(scan.CreateStatement);
                    writer.WriteLine();
                }

                writer.WriteLine("--Drop statements");
                foreach (var scan in _indexScans)
                {
                    writer.WriteLine(scan);
                    writer.WriteLine(scan.DropStatement);
                    writer.WriteLine();
                }
            }
        }
    }
}