using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSpecCollector.Data;
using JobSpecCollector.Interfaces;

namespace JobSpecCollector
{
    public class Collector
    {
        private IJobSpecWebGetter _getter;
        private ILogger _logger;
        private IPersistence _persistence;

        public Collector(IJobSpecWebGetter getter, ILogger logger, IPersistence persistence)
        {
            if (getter == null || logger == null || persistence == null) throw new ArgumentNullException();
            _getter = getter;
            _logger = logger;
            _persistence = persistence;
        }

        public void CollectAndSaveJobSpecs()
        {
            IEnumerable<JobSpec> jobSpecs;
            try {
                jobSpecs = _getter.GetJobSpecs();
            }
            catch(Exception ex)
            {
                _logger.Write("Error in '_getter.GetJobSpecs' (CollectAndSaveJobSpecs)");
                _logger.Write(ex);
                return;
            }
            foreach(var jobSpec in jobSpecs)
            {
                try {
                    _persistence.SaveJobSpec(jobSpec);
                }catch(Exception ex)
                {
                    _logger.Write("Error in '_persistence.SaveJobSpec(jobSpec)' (CollectAndSaveJobSpecs)");
                    _logger.Write(ex);
                }
            }
        }
    }
}
