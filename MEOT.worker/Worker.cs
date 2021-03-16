using System;
using System.Globalization;
using System.IO;
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
            try
            {
                var args = Environment.GetCommandLineArgs();

                _logger = logger;

                var dbPath = args.Length == 2 ? args[1] : null;

                Console.WriteLine($"DB Path: {dbPath}");

                if (!string.IsNullOrEmpty(dbPath) && !File.Exists(dbPath))
                {
                    Console.WriteLine("DB not found - exiting");

                    return;
                }

                _db = new LiteDBDAL(dbPath);

                _settings = _db.SelectOne<Settings>(a => a != null);

                _sourceManager = new SourceManager(_settings);

                new SettingsManager(_db).UpdateSources(_sourceManager.SourceNames);

                _settings = _db.SelectOne<Settings>(a => a != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var malware = _db.SelectAll<Malware>().Where(a => a.Enabled);
                    
                    foreach (var item in malware)
                    {
                        Console.WriteLine($"Checking {item.SHA1}...");

                        var sourceResult = _sourceManager.CheckSources(item.SHA1);

                        item.NumDetections = 0;

                        foreach (var source in sourceResult)
                        {
                            var newCheckpoint = false;

                            var checkpoint = _db.SelectOne<MalwareCheckpoint>(a => a.MalwareId == item.Id);

                            if (checkpoint == null)
                            {
                                newCheckpoint = true;

                                checkpoint = new MalwareCheckpoint
                                {
                                    MalwareId = item.Id,
                                    SourceName = source.Key
                                };
                            }

                            var result = source.Value;

                            checkpoint.Payload = System.Text.Json.JsonSerializer.Serialize(result);
                            checkpoint.Detections = result.Values.Count(a => a.Detected);
                            checkpoint.Vendors = result.Keys.Count;

                            if (newCheckpoint)
                            {
                                _db.Insert(checkpoint);
                            }
                            else
                            {
                                _db.Update(checkpoint);
                            }
                            
                            item.NumDetections += checkpoint.Detections;

                            foreach (var vendor in result.Keys)
                            {
                                var newItem = false;

                                var vendorCheckpoint =
                                    _db.SelectOne<MalwareVendorCheckpoint>(a =>
                                        a.MalwareId == item.Id && a.VendorName == vendor);

                                if (vendorCheckpoint == null)
                                {
                                    newItem = true;

                                    vendorCheckpoint = new MalwareVendorCheckpoint
                                    {
                                        MalwareId = item.Id,
                                        VendorName = vendor
                                    };
                                }

                                vendorCheckpoint.Detected = result[vendor].Detected;

                                if (vendorCheckpoint.Detected)
                                {
                                    if (!vendorCheckpoint.DetectionDate.HasValue)
                                    {
                                        vendorCheckpoint.HoursToDetection =
                                            Math.Round(
                                                result[vendor].DetectedDate.Subtract(item.DayZero.DateTime).TotalHours,
                                                0);

                                        vendorCheckpoint.DetectionDate = result[vendor].DetectedDate;
                                    }
                                } else
                                {
                                    vendorCheckpoint.DetectionDate = null;
                                }

                                if (newItem)
                                {
                                    _db.Insert(vendorCheckpoint);
                                }
                                else
                                {
                                    _db.Update(vendorCheckpoint);
                                }
                            }
                        }

                        _db.Update(item);
                    }

                    // Wait the interval
                    Thread.Sleep(_settings.HoursBetweenChecks * 60 * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}