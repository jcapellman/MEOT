using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

using MEOT.lib.Sources.Base;
using MEOT.lib.Sources.Objects;

namespace MEOT.lib.Sources
{
    public class VirusTotalSource : BaseSource
    {
        private string _vtKey;

        public override string Name => "VirusTotal";

        public override bool RequiresKey => true;

        public override void Initialize(string licenseKey)
        {
            _vtKey = licenseKey;
        }

        public override Dictionary<string, bool> QueryHash(string sha1)
        {
            try
            {
                using var httpClient = new HttpClient();

                var json = httpClient
                    .GetStringAsync(
                        $"https://www.virustotal.com/vtapi/v2/file/report?apikey={_vtKey}&resource={sha1}").Result;


                var fileReport = JsonSerializer.Deserialize<VTFileReport>(json);

                return fileReport == null
                    ? new Dictionary<string, bool>()
                    : fileReport.scans.ToDictionary(vendor => vendor.Key, vendor => vendor.Value.detected);
            }
            catch (Exception ex)
            {
                // TODO: Logging

                return new Dictionary<string, bool>();
            }
        }
    }
}