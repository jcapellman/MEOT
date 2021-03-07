using System.Collections.Generic;
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
            using var httpClient = new HttpClient();

            var json = httpClient
                .GetStringAsync(
                    $"https://www.virustotal.com/vtapi/v2/file/report?apikey={_vtKey}&resource={sha1}").Result;


            var fileReport = JsonSerializer.Deserialize<VTFileReport>(json);

            var vendorAnalysis = new Dictionary<string, bool>();

            // TODO: Parse JSON

            return vendorAnalysis;
        }
    }
}