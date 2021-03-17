using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using MEOT.lib.Containers;
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

        public override SourceContainer QueryHash(string sha1)
        {
            try
            {
                var container = new SourceContainer
                {
                    SourceData = new Dictionary<string, SourceItem>()
                };

                using var httpClient = new HttpClient();

                var json = httpClient
                    .GetStringAsync(
                        $"https://www.virustotal.com/vtapi/v2/file/report?apikey={_vtKey}&resource={sha1}&allinfo=true").Result;


                var fileReport = JsonSerializer.Deserialize<VTFileReport>(json);
                
                if (fileReport == null)
                {
                    return container;
                }

                container.MD5 = fileReport.md5;
                container.SHA256 = fileReport.sha256;
                container.ScanDate = DateTime.ParseExact(fileReport.first_seen, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                foreach (var (vendorName, scanResult) in fileReport.scans)
                {
                    container.SourceData.Add(vendorName, new SourceItem
                    {
                        Detected = scanResult.detected,
                        DetectedDate = DateTime.ParseExact(scanResult.update, "yyyyMMdd", CultureInfo.InvariantCulture),
                        Classification = scanResult.result,
                        VendorVersion = scanResult.version
                    });
                }

                return container;
            }
            catch (Exception ex)
            {
                // TODO: Logging

                return new SourceContainer();
            }
        }
    }
}