using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class NullTests
    {
        // [TestMethod]
        // public void Tokenizer_Null()
        // {
        //     Assert.ThrowsException<ArgumentNullException>(() =>
        //         new DefaultTimeParser(null));
        // }
        
        [TestMethod]
        public void Input_Null_English_Parser()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new EnglishTimeParser().Parse(null));
        }
    }
}