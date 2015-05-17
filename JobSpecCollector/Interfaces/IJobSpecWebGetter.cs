using System.Collections.Generic;
using JobSpecCollector.Data;

namespace JobSpecCollector.Interfaces
{
    public interface IJobSpecWebGetter
    {
        IEnumerable<JobSpec> GetJobSpecs();
    }
}
