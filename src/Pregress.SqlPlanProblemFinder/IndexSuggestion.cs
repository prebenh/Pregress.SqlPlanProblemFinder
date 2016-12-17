using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder
{
    internal class IndexSuggestion
    {
        public string Table { get; set; }
        public string Column { get; set; }

        public XAttribute TableCardinality { get; set; }

        public XAttribute AvgRowSize { get; set; }

        public XAttribute EstimateRows { get; set; }

        public IndexSuggestion(string table, string column, XElement x)
        {
            Table = table.Replace("[", "").Replace("]", "");
            Column = column.Replace("[", "").Replace("]", "");

            if (x != null)
            {
                var relOp = x.Ancestors(Constants.XmlNamespace + "RelOp").First();
                EstimateRows = relOp.Attribute("EstimateRows");
                AvgRowSize = relOp.Attribute("AvgRowSize");
                TableCardinality = relOp.Attribute("TableCardinality");
            }
        }

        public string IndexName => $"IX_{Table}_{Column}_temp";
        public string IfIndexNotExists => $"IF NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.object_id = OBJECT_ID('{Table}') AND i.name = '{IndexName}')";
        public string CreateStatement => $"CREATE NONCLUSTERED INDEX [{IndexName}] ON [{Table}] ([{Column}])";
        public string DropStatement => $"DROP INDEX [{IndexName}] ON [{Table}]";

        public override string ToString()
        {
            return $"-- {TableCardinality} {AvgRowSize} {EstimateRows}";
        }

        public static readonly IndexSuggestion Undefined = new IndexSuggestion(Constants.Undefined, Constants.Undefined, null);

        public override bool Equals(object obj)
        {
            var second = obj as IndexSuggestion;
            if (second == null) return false;

            return Column == second.Column && Table == second.Table;
        }
    }
}