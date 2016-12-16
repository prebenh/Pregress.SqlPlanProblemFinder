using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder
{
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