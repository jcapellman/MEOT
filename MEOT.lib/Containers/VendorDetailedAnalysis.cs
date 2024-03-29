﻿using System;

namespace MEOT.lib.Containers
{
    public class VendorDetailedAnalysis
    {
        public int MalwareId { get; set; }

        public string MalwareName { get; set; }

        public string MalwareType { get; set; }

        public bool Detected { get; set; }

        public double? HoursToDetect { get; set; }

        public DateTime? DetectedDate { get; set; }

        public string Classification { get; set; }

        public string VendorVersion { get; set; }
    }
}