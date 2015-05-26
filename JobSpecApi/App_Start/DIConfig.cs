using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace JobSpecApi.App_Start
{
    public static class DIConfig
    {
        private static Container _conatiner;

        public static void RegisterDI()
        {
            _conatiner = new Container(ce =>
            {
                ce.For<JobSpecCommon.JobSpecDB>().Use<JobSpecDB.DB>();
            });
        }

        public static T Get<T>()
        {
            return _conatiner.GetInstance<T>();
        }
    }
}
