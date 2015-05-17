using System;
using JobSpecCollector;

namespace JobSpecCollectorService
{
    class CollectorService
    {
        private Collector _collector;
        private System.Timers.Timer _timer = new System.Timers.Timer();

        public CollectorService()
        {
            _collector = Resolver.Instance.Get<Collector>();
            _timer.Interval = 1000 * 60 * 30; //30 mins
            _timer.Elapsed += CollectJobspecs;

        }

        public void Start()
        {
            _timer.Stop();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void CollectJobspecs(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_collector == null) throw new Exception("Collector is not initialized");
            _collector.CollectAndSaveJobSpecs();
        }
    }
}
