using System.Collections.Generic;

namespace MEOT.lib.Sources.Objects
{
    public class Exiftool
    {
        public string MIMEType { get; set; }
        public string FileType { get; set; }
        public string WordCount { get; set; }
        public string LineCount { get; set; }
        public string MIMEEncoding { get; set; }
        public string FileTypeExtension { get; set; }
        public string Newlines { get; set; }
    }

    public class Sigcheck
    {
    }

    public class AdditionalInfo
    {
        public string magic { get; set; }
        public List<string> compressed_parents { get; set; }
        public Exiftool exiftool { get; set; }
        public string trid { get; set; }
        public int positives_delta { get; set; }
        public Sigcheck sigcheck { get; set; }
    }

    public class ScanResult
    {
        public bool detected { get; set; }
        public string version { get; set; }
        public string result { get; set; }
        public string update { get; set; }
    }

    public class VTFileReport
    {
        public object vhash { get; set; }
        public List<object> submission_names { get; set; }
        public string scan_date { get; set; }
        public string first_seen { get; set; }
        public int times_submitted { get; set; }
        public AdditionalInfo additional_info { get; set; }
        public int size { get; set; }
        public string scan_id { get; set; }
        public int total { get; set; }
        public int harmless_votes { get; set; }
        public string verbose_msg { get; set; }
        public string sha256 { get; set; }
        public string type { get; set; }
        public List<string> tags { get; set; }
        public object authentihash { get; set; }
        public int unique_sources { get; set; }
        public int positives { get; set; }
        public string ssdeep { get; set; }
        public string md5 { get; set; }
        public string permalink { get; set; }
        public string sha1 { get; set; }
        public string resource { get; set; }
        public int response_code { get; set; }
        public int community_reputation { get; set; }
        public int malicious_votes { get; set; }
        public List<object> ITW_urls { get; set; }
        public string last_seen { get; set; }

        public Dictionary<string, ScanResult> scans { get; set; }
    }


}