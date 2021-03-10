using System.Collections.Generic;

namespace MEOT.lib.Containers
{
    public class VendorContainer
    {
        public int NumDetections { get; set; }

        public int NumAttempts { get; set; }

        public double AverageTimeToDetect { get; set; }
        
        public List<VendorDetailedAnalysis> DetailedAnalysis { get; set; }
    }
}