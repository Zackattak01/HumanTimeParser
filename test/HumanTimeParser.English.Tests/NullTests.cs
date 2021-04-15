using System;
using HumanTimeParser.Core.Parsing.Default;
using HumanTimeParser.Core.Sectioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class NullTests
    {
        [TestMethod]
        public void Input_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new DefaultParser(new EnglishTimeTokenizer(new DefaultSectionizer(null))));
        }
        
        [TestMethod]
        public void Sectionizer_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new DefaultParser(new EnglishTimeTokenizer(null)));
        }
        
        [TestMethod]
        public void Tokenizer_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new DefaultParser(null));
        }
        
        [TestMethod]
        public void Input_Null_English_Parser()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                EnglishTimeParser.Parse(null));
        }
    }
}