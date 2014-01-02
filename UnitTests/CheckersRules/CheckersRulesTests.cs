﻿using System;
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
        public void OneSimpleBlackTest()
        {
            var rules = new Rules("wd4;bd6", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "d6-c5", "d6-e5" }, moves);
        }

        [Test]
        public void ManySimpleTest()
        {
            var rules = new Rules("wa5;wg3;wg7;be3;bh4", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "a5-b6", "g7-f8", "g7-h8", "g3-f4" }, moves);
        }

        [Test]
        public void OneStepTakeSimpleTest()
        {
            var rules = new Rules("wb4;we3;wg3;ba5;bc5;bd4;bf4;bc3;bd2", "w");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "b4-d6", "e3-g5", "g3-e5" }, moves);
        }

        [Test]
        public void ChainTakeSimpleTest()
        {
            var rules = new Rules("bb2;wc3;we5;we3", "b");
            var moves = rules.GetMoveList();
            CollectionAssert.AreEquivalent(new List<string> { "b2-d4-f6", "b2-d4-f2" }, moves);
        }
    }
}
