using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSpecApi.Controllers;
using JobSpecApi.Models;
using Moq;
using JobSpecCommon;
using JobSpecCommon.Dto;

namespace JobSpecApi.Tests.Controllers
{
    [TestFixture]
    public class JobSpecsControllerTest
    {
        [SetUp]
        public void Setup()
        {
            JobSpecApi.App_Start.DIConfig.RegisterDI();
        }

        [Test]
        public void Should_instantiate_without_issue()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act

            //assert
            Assert.NotNull(c);
        }

        [Test]
        public void Should_give_empty_result_for_null()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(null);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual(false, result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_empty_skill_list()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = "",
                LastNDays = 7,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_empty_skill_list_2()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = ";;;;",
                LastNDays = 7,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_zero_lastndays()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = 0,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_negative_lastndays()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = -5,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_zero_resultdays()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = 7,
                ResultDays = 0
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_empty_result_for_negative_resultdays()
        {
            //arrange
            var c = new JobSpecApi.Controllers.JobSpecsController();

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = 7,
                ResultDays = -9
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(0, result.Data.Count());
        }

        [Test]
        public void Should_give_non_empty_result_proper_parameters()
        {
            //arrange
            var data = new List<ReturnData>
            {
                new ReturnData{Skill = "c#", Date = new DateTime(2015, 4, 20), Count = 4},
                new ReturnData{Skill = ".net", Date = new DateTime(2015, 4, 21), Count = 5}
            };
            var db = new Mock<JobSpecDB>();
            db.Setup(o => o.Query(It.IsAny<string[]>(), It.IsAny<int>(), It.IsAny<int>())).Returns(data);
            var c = new JobSpecApi.Controllers.JobSpecsController(db.Object);

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = 7,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(data, result.Data);
        }

        [Test]
        public void Should_have_error_if_exception()
        {
            //arrange
            var db = new Mock<JobSpecDB>();
            db.Setup(o => o.Query(It.IsAny<string[]>(), It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();
            var c = new JobSpecApi.Controllers.JobSpecsController(db.Object);

            //act
            var result = c.Get(new CallParams
            {
                Skills = "c#",
                LastNDays = 7,
                ResultDays = 90
            });

            //assert
            Assert.NotNull(c);
            Assert.IsTrue(result.HasError);
            Assert.IsTrue(result.ErrorMessages.Count > 0);
        }
    }
}
