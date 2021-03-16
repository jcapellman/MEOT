﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

        public override Dictionary<string, SourceItem> QueryHash(string sha1)
        {
            try
            {
                using var httpClient = new HttpClient();

                var json = httpClient
                    .GetStringAsync(
                        $"https://www.virustotal.com/vtapi/v2/file/report?apikey={_vtKey}&resource={sha1}").Result;


                var fileReport = JsonSerializer.Deserialize<VTFileReport>(json);

                var response = new Dictionary<string, SourceItem>();
                
                if (fileReport == null)
                {
                    return response;
                }
                
                foreach (var (vendorName, scanResult) in fileReport.scans)
                {
                    response.Add(vendorName, new SourceItem
                    {
                        Detected = scanResult.detected,
                        DetectedDate = DateTime.ParseExact(scanResult.update, "yyyyMMdd", CultureInfo.InvariantCulture),
                        Classification = scanResult.result,
                        VendorVersion = scanResult.version
                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                // TODO: Logging

                return new Dictionary<string, SourceItem>();
            }
        }
    }
}