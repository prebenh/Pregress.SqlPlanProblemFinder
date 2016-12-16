using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder.IndexCreators
{
    internal class ColumnReference : IPredicate
    {
        private readonly XElement _element;

        public ColumnReference(XElement element)
        {
            _element = element;
        }

        public IndexSuggestion Run()
        {
            var columReference = _element
                .Descendants(Constants.XmlNamespace + "ColumnReference")
                .FirstOrDefault(x =>
                    x.Attributes("Table").Any()
                    && x.Attributes("Column").Any());

            if (columReference != null)
            {
                var table = columReference.Attributes("Table").First().Value;
                var column = columReference.Attributes("Column").First().Value;

                return new IndexSuggestion(table, column, _element);
            }

            return IndexSuggestion.Undefined;
        }
    }
}