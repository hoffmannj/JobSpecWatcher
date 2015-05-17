using System;
using StructureMap;
using JobSpecCollector.Interfaces;
using JobSpecCollector.Implementations;
using System.Configuration;

namespace JobSpecCollectorService
{
    internal class Resolver
    {
        private Container _container;

        private static Lazy<Resolver> _instance = new Lazy<Resolver>(() => new Resolver(), true);

        private Resolver()
        {
            Initialize();
        }

        private void Initialize()
        {
            _container = new Container(e =>
            {
                e.For<ILogger>().Use<Logger>();
                e.For<IJobSpecWebGetter>().Use<WebGetter>();
                e.For<IPersistence>().Use<Persistence>().Ctor<string>("connString").Is(GetConnectionString());
                e.For<ITextSplitter>().Use<TextSplitter>();
            });
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["defaultConnStr"].ConnectionString;
        }

        public static Resolver Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public T Get<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
