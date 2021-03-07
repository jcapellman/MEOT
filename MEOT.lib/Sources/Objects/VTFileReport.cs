using System.Collections.Generic;

namespace MEOT.lib.Sources.Objects
{
    public class VTFileReport
    {
        public int response_code { get; set; }
        public string verbose_msg { get; set; }
        public string resource { get; set; }
        public string scan_id { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
        public string scan_date { get; set; }
        public string permalink { get; set; }
        public int positives { get; set; }
        public int total { get; set; }
        public List<VendorResult> scans { get; set; }
       
        public class VendorResult
        {
            public string Name { get; set; }

            public Result result { get; set; }
        }

        public class Result
        {
            public bool detected { get; set; }

            public string version { get; set; }

            public string result { get; set; }

            public string update { get; set; }
        }
    }
}