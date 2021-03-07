using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MEOT.lib.DAL.Base;
using MEOT.lib.Objects;

namespace MEOT.worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private Settings _settings;

        private IDAL _db;

        public Worker(ILogger<Worker> logger, IDAL db)
        {
            _logger = logger;

            _db = db;

            _settings = _db.SelectFirstOrDefault<Settings>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var malware = _db.SelectAll<Malware>().Where(a => a.Enabled);

                foreach (var item in malware)
                {
                    // TODO: Iterate through sources and create MalwareCheckpoints
                    // TODO: Update the root malware object
                }
                
                // Wait the interval
                await Task.Delay(_settings.HoursBetweenChecks * 60 * 60 * 1000, stoppingToken);
            }
        }
    }
}