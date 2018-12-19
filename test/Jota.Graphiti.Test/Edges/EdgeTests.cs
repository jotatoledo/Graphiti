// <copyright file="EdgeTests.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti.Test
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class EdgeTests
    {
        public static IEnumerable<object[]> ValueTypeData => new[]
        {
            new object[] { 'a', 'b' },
            new object[] { 2.0, 1.0 },
            new object[] { 1, 2 },
            new object[] { "a", "b" }
        };

        [Fact]
        public void ThrowsIfAnyVertexIsNull()
        {
            Action nullSource = () => new Edge<string>(null, string.Empty);
            Action nullTarget = () => new Edge<string>(string.Empty, null);

            nullSource.Should().Throw<ArgumentNullException>();
            nullTarget.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        [Trait("VertexType", "ValueType")]
        public void Equals_Should_ReturnTrue_When_EquatingAgainstsCaller<TVertex>(TVertex source, TVertex target)
        {
            var sut = this.CreateSystemUnderTest(source, target);

            var result = sut.Equals(sut);

            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        [Trait("VertexType", "ValueType")]
        public void Equals_Should_ReturnTrue_When_EquatingAgainstEdgeWithSameVertices<TVertex>(TVertex source, TVertex target)
        {
            var sut = this.CreateSystemUnderTest(source, target);

            var result = sut.Equals(this.CreateSystemUnderTest(source, target));

            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        [Trait("VertexType", "ValueType")]
        public void Equals_Should_ReturnFalse_When_EquatingAgainstEdgeWithDifferentVertices<TVertex>(TVertex source, TVertex target)
        {
            var sut = this.CreateSystemUnderTest(source, target);

            var result = sut.Equals(this.CreateSystemUnderTest(target, source));

            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_Should_HandleTypeWithNoEqualsOverride()
        {
            var testId = 1;
            TestTypeWithoutEquals typeFactory(int val) => new TestTypeWithoutEquals(val);
            var sut = this.CreateSystemUnderTest(typeFactory(testId), typeFactory(testId));

            var equalsToCaller = sut.Equals(sut);
            var equalsToOtherWithSameVertices = sut.Equals(this.CreateSystemUnderTest(typeFactory(testId), typeFactory(testId)));

            equalsToCaller
                .Should()
                .BeTrue("because default `Equals` equates references and the caller was passed as argument");
            equalsToOtherWithSameVertices
                .Should()
                .BeFalse("because default `Equals` equates references and a new instance was passed as argument");
        }

        [Fact]
        public void Equals_Should_HandleTypeWithEqualsOverride()
        {
            var testId = 1;
            TestTypeWithEquals typeFactory(int val) => new TestTypeWithEquals(val);
            var sut = this.CreateSystemUnderTest(typeFactory(testId), typeFactory(testId));

            var equalsToCaller = sut.Equals(sut);
            var equalsToOtherWithSameVertices = sut.Equals(this.CreateSystemUnderTest(typeFactory(testId), typeFactory(testId)));

            equalsToCaller
                .Should()
                .BeTrue("because overriden `Equals` equates references and the caller was passed as argument");
            equalsToOtherWithSameVertices
                .Should()
                .BeTrue("because overriden `Equals` considers properties and a new instance with same props. was passed as argument");
        }

        private Edge<TVertex> CreateSystemUnderTest<TVertex>(TVertex source, TVertex target)
        {
            return new Edge<TVertex>(source, target);
        }

        private sealed class TestTypeWithoutEquals
        {
            public TestTypeWithoutEquals(int id)
            {
                this.Id = id;
            }

            public int Id { get; }
        }

        private sealed class TestTypeWithEquals
        {
            public TestTypeWithEquals(int id)
            {
                this.Id = id;
            }

            public int Id { get; }

            public override int GetHashCode() => this.Id.GetHashCode();

            public override bool Equals(object obj) => obj is TestTypeWithEquals other
                    && (other == this || this.Id.Equals(other.Id));
        }
    }
}
