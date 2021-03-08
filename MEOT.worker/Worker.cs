using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MEOT.lib.DAL;
using MEOT.lib.DAL.Base;
using MEOT.lib.Managers;
using MEOT.lib.Objects;

namespace MEOT.worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly Settings _settings;

        private IDAL _db;

        private readonly SourceManager _sourceManager;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            _db = new LiteDBDAL();

            _settings = _db.SelectFirstOrDefault<Settings>();

            _sourceManager = new SourceManager(_settings);

            new SettingsManager(_db).UpdateSources(_sourceManager.SourceNames);

            _settings = _db.SelectFirstOrDefault<Settings>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var malware = _db.SelectAll<Malware>().Where(a => a.Enabled);

                foreach (var item in malware)
                {
                    var sourceResult = _sourceManager.CheckSources(item.SHA1);

                    item.NumDetections = 0;

                    foreach (var source in sourceResult)
                    {
                        var checkpoint = new MalwareCheckpoint
                        {
                            MalwareId = item.Id, 
                            SourceName = source.Key
                        };
                        
                        var result = source.Value;

                        checkpoint.Payload = System.Text.Json.JsonSerializer.Serialize(result);
                        checkpoint.Detections = result.Values.Count(a => a);
                        checkpoint.Vendors = result.Keys.Count;

                        _db.Insert(checkpoint);

                        item.NumDetections += checkpoint.Detections;

                        foreach (var vendorCheckpoint in from vendor in result.Keys select new MalwareVendorCheckpoint
                        {
                            MalwareId = item.Id,
                            HoursToDetection = DateTimeOffset.Now.Subtract(item.DayZero).TotalHours,
                            VendorName = vendor,
                            Detected = result[vendor]
                        })
                        {
                            _db.Insert(vendorCheckpoint);
                        }
                    }
                    
                    _db.Update(item);
                }
                
                // Wait the interval
                await Task.Delay(_settings.HoursBetweenChecks * 60 * 60 * 1000, stoppingToken);
            }
        }
    }
}