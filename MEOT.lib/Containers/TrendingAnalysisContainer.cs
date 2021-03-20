using System.Collections.Generic;

namespace MEOT.lib.Containers
{
    public class TrendingAnalysisContainer
    {
        public List<VendorAnalysis> TopVendors { get; set; }

        public List<VendorAnalysis> WorstVendors { get; set; }
    }
}