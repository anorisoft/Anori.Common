// -----------------------------------------------------------------------
// <copyright file="ListExtensionsUnitTests.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common.UnitTests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis.VisualBasic;

    using NUnit.Framework;

    public class ResetLazyUnitTests
    {
        [Test]
        public void ResetLazy_Value_Return0()
        {
            var count = 0;
            var lazy = new ResetLazy<int>(() => ++count);

            var actual = lazy.Value;
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void ResetLazy_DoubleGet_Value_Return0()
        {
            var count = 0;
            var lazy = new ResetLazy<int>(() => ++count);

            var actual = lazy.Value;
            Assert.AreEqual(1, actual);
            actual = lazy.Value;
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void ResetLazy_Reset_Value_Return0()
        {
            var count = 0;
            var lazy = new ResetLazy<int>(() => ++count);

            var actual = lazy.Value;
            Assert.AreEqual(1, actual);
            lazy.Reset();
            actual = lazy.Value;
            Assert.AreEqual(2, actual);
        }


        [Test]
        public void ResetLazy_Load_Value_Return0()
        {
            var count = 0;
            var lazy = new ResetLazy<int>(() => ++count);
            Assert.AreEqual(0, count);
            lazy.Load();
            Assert.AreEqual(1, count);
            var actual = lazy.Value;
            Assert.AreEqual(1, actual);
        }
    }
}