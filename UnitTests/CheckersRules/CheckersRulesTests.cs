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
            var rules = new Rules("wd4", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string>{"d4-c5", "d4-e5"}, moves);
        }

        [Test]
        public void ManySimpleTest()
        {
            var rules = new Rules("wa5;wg3;wg7;be3;bh4", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "a5-b6", "g7-f8", "g7-h8", "g3-f4" }, moves);
        }
    }
}
