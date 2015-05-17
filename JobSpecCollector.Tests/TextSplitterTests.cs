using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSpecCollector.Implementations;
using NUnit.Framework;

namespace JobSpecCollector.Tests
{
    [TestFixture]
    public class TextSplitterTests
    {
        [Test]
        public void Should_instantiate_without_exception()
        {
            var ts = new TextSplitter();
            Assert.NotNull(ts);
        }

        [Test]
        public void Should_return_empty_for_null()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords(null);
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void Should_return_empty_for_empty_string()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords("");
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void Should_return_one_word()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords("word");
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void Should_return_one_word_2()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords("first-Second");
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void Should_return_two_words()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords("First. Second.");
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Should_return_two_words_2()
        {
            var ts = new TextSplitter();
            var result = ts.ToWords("First:Second");
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
