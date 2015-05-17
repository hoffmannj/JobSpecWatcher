using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using JobSpecCollector.Interfaces;

namespace JobSpecCollector.Tests
{
    [TestFixture]
    public class CollectorTests
    {
        [Test]
        public void Should_instantiate_without_exception()
        {
            var logger = new Mock<ILogger>();
            var persistence = new Mock<IPersistence>();
            var getter = new Mock<IJobSpecWebGetter>();
            var c = new Collector(getter.Object, logger.Object, persistence.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_if_getter_is_missing()
        {
            var logger = new Mock<ILogger>();
            var persistence = new Mock<IPersistence>();
            var c = new Collector(null, logger.Object, persistence.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_if_logger_is_missing()
        {
            var persistence = new Mock<IPersistence>();
            var getter = new Mock<IJobSpecWebGetter>();
            var c = new Collector(getter.Object, null, persistence.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_if_persistence_is_missing()
        {
            var logger = new Mock<ILogger>();
            var getter = new Mock<IJobSpecWebGetter>();
            var c = new Collector(getter.Object, logger.Object, null);
        }

        [Test]
        public void Should_save_jobspecs_when_no_error_getting_them()
        {
            var jobSpecs = new List<Data.JobSpec>
            {
                new Data.JobSpec
                {
                    Description = "some test jobspec",
                    Guid = new Guid("B578626F-AEFE-46C1-A2E2-EC66D76745D7"),
                    Link = "some link",
                    PubDate = new DateTime(2015, 1, 1),
                    Source="source name",
                    SpecText="jobspec text",
                    Title="jobspec title"
                }
            };

            var logger = new Mock<ILogger>();
            var persistence = new Mock<IPersistence>();

            var getter = new Mock<IJobSpecWebGetter>();
            getter.Setup(g => g.GetJobSpecs()).Returns(jobSpecs);

            var c = new Collector(getter.Object, logger.Object, persistence.Object);
            c.CollectAndSaveJobSpecs();

            persistence.Verify(p => p.SaveJobSpec(jobSpecs[0]));
        }

        [Test]
        public void Should_log_if_error_in_getting_specs()
        {
            var logger = new Mock<ILogger>();
            var persistence = new Mock<IPersistence>();
            var getter = new Mock<IJobSpecWebGetter>();
            var exception = new Exception();
            getter.Setup(g => g.GetJobSpecs()).Throws(exception);

            var c = new Collector(getter.Object, logger.Object, persistence.Object);
            c.CollectAndSaveJobSpecs();
            logger.Verify(l => l.Write(exception));
        }

        [Test]
        public void Should_log_if_error_in_saving_specs()
        {
            var jobSpecs = new List<Data.JobSpec>
            {
                new Data.JobSpec
                {
                    Description = "some test jobspec",
                    Guid = new Guid("B578626F-AEFE-46C1-A2E2-EC66D76745D7"),
                    Link = "some link",
                    PubDate = new DateTime(2015, 1, 1),
                    Source="source name",
                    SpecText="jobspec text",
                    Title="jobspec title"
                },
                new Data.JobSpec
                {
                    Description = "some test jobspec 2",
                    Guid = new Guid("B578626F-AEFE-46C1-A2E2-EC66D76745D1"),
                    Link = "some link 2",
                    PubDate = new DateTime(2015, 2, 2),
                    Source="source name 2",
                    SpecText="jobspec text 2",
                    Title="jobspec title 2"
                }
            };

            var exception = new Exception();
            var logger = new Mock<ILogger>();
            var persistence = new Mock<IPersistence>();
            persistence.Setup(p => p.SaveJobSpec(jobSpecs[1])).Throws(exception);

            var getter = new Mock<IJobSpecWebGetter>();
            getter.Setup(g => g.GetJobSpecs()).Returns(jobSpecs);

            var c = new Collector(getter.Object, logger.Object, persistence.Object);
            c.CollectAndSaveJobSpecs();
            logger.Verify(l => l.Write(exception));
        }
    }
}
