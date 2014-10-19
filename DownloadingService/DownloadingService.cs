using DataSyncService;
using System;
using System.Timers;

namespace DownloadingService
{
    internal class DownloadingService
    {
        private Timer _timer;
        private ISyncDataService _syncDataService;

        public DownloadingService(ISyncDataService syncDataService)
        {
            _syncDataService = syncDataService;
            var invokeInterval = TimeSpan.FromMinutes(30).TotalMilliseconds;
            _timer = new Timer(invokeInterval) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) =>
            {
                if (!_syncDataService.Locked)
                    _syncDataService.Sync();
            };
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
