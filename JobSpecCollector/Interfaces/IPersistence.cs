using JobSpecCollector.Data;

namespace JobSpecCollector.Interfaces
{
    public interface IPersistence
    {
        void SaveJobSpec(JobSpec jobSpec);
    }
}
