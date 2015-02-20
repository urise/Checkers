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
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("wd4", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string>{"d4-c5", "d4-e5"}, moves);
        }

        [Test]
        public void OneSimpleBlackTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("wd4;bd6", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "d6-c5", "d6-e5" }, moves);
        }

        [Test]
        public void ManySimpleTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("wa5;wg3;wg7;be3;bh4", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "a5-b6", "g7-f8", "g7-h8", "g3-f4" }, moves);
        }

        [Test]
        public void OneStepTakeSimpleTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("wb4;we3;wg3;ba5;bc5;bd4;bf4;bc3;bd2", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "b4-d6", "e3-g5", "g3-e5", "e3-c1" }, moves);
        }

        [Test]
        public void ChainTakeSimpleTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("bb2;wc3;we5;we3", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "b2-d4-f6", "b2-d4-f2" }, moves);
        }

        [Test]
        public void ChainTakeSimpleTest2()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("wd4;be5;bg5;bg3;be3;bc5", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "d4-f6-h4-f2-d4-b6", "d4-f2-h4-f6-d4-b6", "d4-b6" }, moves);
        }

        [Test]
        public void KingTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("Wc3;ba5", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { 
                "c3-b2", "c3-a1", "c3-d4", "c3-e5", "c3-f6", "c3-g7", "c3-h8", "c3-b4", "c3-d2", "c3-e1"}, moves);
        }

        [Test]
        public void KingSingleTakeTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("We1;bc3", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "e1-b4", "e1-a5"}, moves);
        }

        [Test]
        public void KingSingleTakeTest2()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("We1;bc3;wa5", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "e1-b4" }, moves);
        }

        [Test]
        public void KingChainTakeTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("Bb2;wd4;Wd6;Wb8;Wf4;wg5", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "b2-e5-c7", "b2-e5-g3", "b2-e5-h2", 
                "b2-f6-h4", "b2-g7", "b2-h8" }, moves);
        }

        [Test]
        public void SimpleToKingChainTakeTest()
        {
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules("bc5;wb4;wb2;we3;wf6", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(
                new List<string> { "c5-a3-c1-f4", "c5-a3-c1-g5-e7", "c5-a3-c1-g5-d8", "c5-a3-c1-h6" }, moves);
        }
    }
}
