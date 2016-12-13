using System;
using System.IO;
using System.Xml.Linq;

namespace SqlPlanProblemFinder
{
    internal class SqlPlanLoader
    {
        public XDocument Load(string path)
        {
            if (!path.EndsWith(".sqlplan", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("File extensions should be .sqlplan");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File cannot be found", path);
            }

            return XDocument.Load(path);
        }
    }
}