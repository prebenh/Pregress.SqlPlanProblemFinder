using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder
{
    internal class FindIndexScans
    {
        private readonly XDocument _document;

        public FindIndexScans(XDocument document)
        {
            _document = document;
        }

        public IEnumerable<IndexScan> Find()
        {
            var indexScans = _document.Descendants("{http://schemas.microsoft.com/sqlserver/2004/07/showplan}IndexScan");
            return indexScans.Where(x => x.Parent != null
                                         && !x.Parent.Attribute("LogicalOp").Value.Contains("Seek"))
                .OrderByDescending(x => int.Parse(x.Parent.Attribute("TableCardinality").Value))
                .Select(x => new IndexScan(x));
        }
    }

    internal class FindTableScans
    {
        private readonly XDocument _document;

        public FindTableScans(XDocument document)
        {
            _document = document;
        }

        public IEnumerable<IndexScan> Find()
        {
            var tableScans = _document.Descendants("{http://schemas.microsoft.com/sqlserver/2004/07/showplan}TableScan");
            return tableScans.Where(x => x.Parent != null)
                .Select(x => new IndexScan(x));
        }
    }
}