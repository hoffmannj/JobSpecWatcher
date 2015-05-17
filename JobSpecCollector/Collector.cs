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
            if (!GetJobSpecs(out jobSpecs)) return;
            foreach (var jobSpec in jobSpecs)
            {
                SaveJobSpec(jobSpec);
            }
        }

        private bool GetJobSpecs(out IEnumerable<JobSpec> jobSpecs)
        {
            try
            {
                jobSpecs = _getter.GetJobSpecs();
                return true;
            }
            catch (Exception ex)
            {
                jobSpecs = null;
                LogException(ex, "Error in '_getter.GetJobSpecs' (GetJobSpecs)");
                return false;
            }
        }

        private void SaveJobSpec(JobSpec jobSpec)
        {
            try
            {
                _persistence.SaveJobSpec(jobSpec);
            }
            catch (Exception ex)
            {
                LogException(ex, "Error in '_persistence.SaveJobSpec(jobSpec)' (SaveJobSpec)");
            }
        }

        private void LogException(Exception ex, string message)
        {
            _logger.Write(message);
            _logger.Write(ex);
        }
    }
}
