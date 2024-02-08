﻿using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using SkbKontur.NUnit.Middlewares;

namespace NUnit.Middlewares.Tests
{
    [TestFixture(-1)]
    [TestFixture(-2)]
    [TestFixture(-3)]
    [TestFixture(-4)]
    [TestFixture(-5)]
    [TestFixture(-6)]
    [TestFixture(-7)]
    [TestFixture(-8)]
    [TestFixture(-9)]
    [TestFixture(-10)]
    [TestFixtureSource(nameof(testCases))]
    public class ParallelParametrizedTestFixtureTest : SimpleTestBase
    {
        private readonly int i;

        public ParallelParametrizedTestFixtureTest(int i)
        {
            this.i = i;
        }

        protected override void Configure(ISetupBuilder fixture, ISetupBuilder test)
        {
            fixture
                .UseSetup<TestInvocationCounterSetup>()
                .Use(t => t.Get<Counter>().InvocationsCount += i);
        }

        [Test]
        public void Test()
        {
            var counter = SimpleTestContext.Current.Get<Counter>();
            counter.Should().NotBeNull();

            counter.InvocationsCount.Should().Be(i);
        }

        private static readonly TestFixtureData[] testCases = Enumerable.Range(0, 100).Select(x => new TestFixtureData(x)).ToArray();
    }
}