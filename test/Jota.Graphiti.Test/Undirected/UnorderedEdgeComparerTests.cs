// <copyright file="UnorderedEdgeComparerTests.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class UnorderedEdgeComparerTests
    {
        public static IEnumerable<object[]> Edges => new[]
        {
            new object[] { TestEdge.Wrap('a', 'b') },
            new object[] { new Edge<char>('a', 'b') },
            new object[] { new Edge<double>(1.0, 2.0) },
            new object[] { new Edge<int>(1, 0) }
        };

        [Theory]
        [MemberData(nameof(Edges))]
        public void Equals_Should_ReturnTrue_When_EquatingSameEdges<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, edge);

            result.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void Equals_Should_ReturnTrue_When_EquatingDifferentEdgesWithSameVertices<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, CloneEdge(edge));

            result.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void Equals_Should_ReturnTrue_When_EquatingAgainstEdgeWithInvertedOrder<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, edge.Invert());

            result.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void Should_GenerateSameHashCode_When_HashingInvertedEdges<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();
            var expectedHashCode = sut.GetHashCode(edge.Invert());

            var edgeHashCode = sut.GetHashCode(edge);

            edgeHashCode.Should()
                .Be(expectedHashCode);
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void Should_GenerateSameHashCode_When_HashingSameEdgeMultipleTimes<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var edgeHashCode = sut.GetHashCode(edge);

            edgeHashCode.Should()
                .Be(sut.GetHashCode(edge));
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void Should_GenerateSameHashCode_When_HashingDifferentEdgesWithSameVertices<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();
            var edgeWithSameVertices = edge.Invert().Invert();
            var expectedHashCode = sut.GetHashCode(edgeWithSameVertices);

            var edgeHashCode = sut.GetHashCode(edge);

            edge.Should()
                .NotBeSameAs(edgeWithSameVertices);
            edgeHashCode.Should()
                .Be(expectedHashCode);
        }

        private static IEdge<TVertex> CloneEdge<TVertex>(IEdge<TVertex> edge)
        {
            return CreateEdge(edge.Source, edge.Target);
        }

        private static IEdge<TVertex> CreateEdge<TVertex>(TVertex source, TVertex target)
        {
            return new Edge<TVertex>(source, target);
        }

        private UnorderedEdgeComparer<TVertex, IEdge<TVertex>> CreateSystemUnderTest<TVertex>()
        {
            return new UnorderedEdgeComparer<TVertex, IEdge<TVertex>>();
        }
    }
}
