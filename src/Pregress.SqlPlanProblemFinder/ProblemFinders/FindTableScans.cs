using Pregress.SqlPlanProblemFinder.IndexCreators;
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

        public IEnumerable<IndexSuggestion> Find()
        {
            var tableScans = _document.Descendants(Constants.XmlNamespace + "TableScan");
            return tableScans.Where(x => x.Parent != null)
                .Select(x => new PredicateFactory(x).Run());
        }
    }
}