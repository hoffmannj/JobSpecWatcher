using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSpecCollector.Data;

namespace JobSpecCollector.Interfaces
{
    public interface IJobSpecWebGetter
    {
        IEnumerable<JobSpec> GetJobSpecs();
    }
}
