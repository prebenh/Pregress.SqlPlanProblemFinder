using System.Collections.Generic;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder.IndexCreators
{
    internal class PredicateFactory
    {
        private List<IPredicate> _predicates;

        public PredicateFactory(XElement element)
        {
            _predicates = new List<IPredicate>
            {
                new ScalarOperator(element),
                new DefinedValues(element),
            };
        }

        public IndexSuggestion Run()
        {
            foreach (var predicate in _predicates)
            {
                var result = predicate.Run();
                if (result != IndexSuggestion.Undefined)
                    return result;
            }

            return IndexSuggestion.Undefined;
        }
    }
}