using System;

namespace MEOT.lib.Sources.Objects
{
    public class SourceItem
    {
        public bool Detected { get; set; }

        public DateTime DetectedDate { get; set; }

        public string Classification { get; set; }

        public string VendorVersion { get; set; }

        public string MD5 { get; set; }
    }
}