using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public Worker(ILogger<Worker> logger, IDAL db)
        {
            _logger = logger;

            _db = db;

            _settings = _db.SelectFirstOrDefault<Settings>();

            _sourceManager = new SourceManager(_settings);
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
                    }
                    
                    _db.Update(item);
                }
                
                // Wait the interval
                await Task.Delay(_settings.HoursBetweenChecks * 60 * 60 * 1000, stoppingToken);
            }
        }
    }
}