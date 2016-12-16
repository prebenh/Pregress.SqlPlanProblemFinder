using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder.IndexCreators
{
    internal class ScalarOperator : IPredicate
    {
        private readonly XElement _element;

        public ScalarOperator(XElement element)
        {
            _element = element;
        }

        public IndexSuggestion Run()
        {
            var scalarOperator = _element.Descendants(Constants.XmlNamespace + "ScalarOperator").FirstOrDefault();

            if (scalarOperator != null)
            {
                return new ColumnReference(scalarOperator).Run();
            }

            return IndexSuggestion.Undefined;
        }
    }

    internal class DefinedValues : IPredicate
    {
        private readonly XElement _element;

        public DefinedValues(XElement element)
        {
            _element = element;
        }

        public IndexSuggestion Run()
        {
            var definedValues = _element.Descendants(Constants.XmlNamespace + "DefinedValue").FirstOrDefault();

            if (definedValues != null)
            {
                return new ColumnReference(_element).Run();
            }

            return IndexSuggestion.Undefined;
        }
    }
}