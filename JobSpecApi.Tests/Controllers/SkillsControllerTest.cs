using JobSpecApi.Controllers;
using JobSpecCommon;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSpecApi.Tests.Controllers
{
    [TestFixture]
    public class SkillsControllerTest
    {
        [SetUp]
        public void Setup()
        {
            JobSpecApi.App_Start.DIConfig.RegisterDI();
        }

        [Test]
        public void Should_instantiate_without_error()
        {
            //arrange
            var c = new SkillsController();

            //act

            //assert
            Assert.NotNull(c);
        }

        [Test]
        public void Should_give_result_if_no_error()
        {
            //arrange
            var skills = new List<string> { "c#", ".net" };
            var db = new Mock<JobSpecDB>();
            db.Setup(o => o.GetSkills()).Returns(skills);
            var c = new SkillsController(db.Object);

            //act
            var result = c.Get();

            //assert
            Assert.NotNull(c);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(skills, result.Data);
        }

        [Test]
        public void Should_have_error_if_exception()
        {
            //arrange
            var db = new Mock<JobSpecDB>();
            db.Setup(o => o.GetSkills()).Throws<Exception>();
            var c = new SkillsController(db.Object);

            //act
            var result = c.Get();

            //assert
            Assert.NotNull(c);
            Assert.IsTrue(result.HasError);
            Assert.IsTrue(result.ErrorMessages.Count > 0);
        }
    }
}
