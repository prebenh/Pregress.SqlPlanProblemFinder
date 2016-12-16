namespace Pregress.SqlPlanProblemFinder.IndexCreators
{
    internal interface IPredicate
    {
        IndexSuggestion Run();
    }
}