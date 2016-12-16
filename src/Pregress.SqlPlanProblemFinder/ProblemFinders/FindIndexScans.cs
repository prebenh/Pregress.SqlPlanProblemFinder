using Pregress.SqlPlanProblemFinder.IndexCreators;
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

        public IEnumerable<IndexSuggestion> Find()
        {
            var indexScans = _document.Descendants(Constants.XmlNamespace + "IndexScan");
            return indexScans.Where(x => x.Parent != null
                                         && !x.Parent.Attribute("LogicalOp").Value.Contains("Seek"))
                .OrderByDescending(x => int.Parse(x.Parent.Attribute("TableCardinality").Value))
                .Select(x => new PredicateFactory(x).Run());
        }
    }
}