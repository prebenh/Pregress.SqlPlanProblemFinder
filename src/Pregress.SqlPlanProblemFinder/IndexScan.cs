using System.Linq;
using System.Xml.Linq;

namespace Pregress.SqlPlanProblemFinder
{
    internal struct IndexScan
    {
        public string Table { get; set; }
        public string Column { get; set; }

        public XAttribute TableCardinality { get; set; }

        public XAttribute AvgRowSize { get; set; }

        public XAttribute EstimateRows { get; set; }

        public IndexScan(XElement x)
        {
            var parent = x.Parent;
            EstimateRows = parent.Attribute("EstimateRows");
            AvgRowSize = parent.Attribute("AvgRowSize");
            TableCardinality = parent.Attribute("TableCardinality");

            var scalarString = x.Descendants(
                "{http://schemas.microsoft.com/sqlserver/2004/07/showplan}ScalarOperator")
                .Attributes("ScalarString").First().Value.Replace("[", "").Replace("]", "").Split('.');

            Table = scalarString[2];
            Column = scalarString[3].Remove(scalarString[3].IndexOf('='));
        }

        public string CreateStatement => $"CREATE NONCLUSTERED INDEX [IX_{Table}_{Column}] ON [{Table}] ([{Column}])";
        public string DropStatement => $"DROP INDEX [IX_{Table}_{Column}] ON [{Table}]";

        public override string ToString()
        {
            return $"-- {TableCardinality} {AvgRowSize} {EstimateRows}";
        }
    }
}