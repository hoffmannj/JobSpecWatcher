using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace JobSpecCollectorService
{
    class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<CollectorService>(SerViceConfigurator);
                x.RunAsNetworkService();
                x.SetDescription("Indeed jobspec collector");
                x.SetDisplayName("JobSpecCollector");
                x.SetServiceName("JobSpecCollector");
            });
        }

        private static void SerViceConfigurator(ServiceConfigurator<CollectorService> sc)
        {
            sc.ConstructUsing(name => new CollectorService());
            sc.WhenStarted(tc => tc.Start());
            sc.WhenStopped(tc => tc.Stop());
        }
    }
}
