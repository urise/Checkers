using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CheckersRules;

namespace UnitTests.CheckersRules
{
    [TestFixture]
    class CheckersRulesTests
    {
        [Test]
        public void OneSimpleTest()
        {
            var rules = new Rules("w wd4");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string>{"c5", "e5"}, moves);
        }
    }
}
