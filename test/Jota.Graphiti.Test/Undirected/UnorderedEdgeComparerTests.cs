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
        public static IEnumerable<object[]> ValueTypeData => new[]
        {
            new object[] { new Edge<char>('a', 'b') },
            new object[] { new Edge<double>(1.0, 2.0) },
            new object[] { new Edge<int>(1, 0) },
            new object[] { new Edge<string>("a", "b") }
        };

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        public void Equals_Should_ReturnTrue_When_EquatingSameEdges<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, edge);

            result.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        public void Equals_Should_ReturnTrue_When_EquatingDifferentEdgesWithSameVertices<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, CloneEdge(edge));

            result.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValueTypeData))]
        public void Equals_Should_ReturnTrue_When_EquatingAgainstEdgeWithInvertedOrder<TVertex>(IEdge<TVertex> edge)
        {
            var sut = this.CreateSystemUnderTest<TVertex>();

            var result = sut.Equals(edge, InvertEdge(edge));

            result.Should()
                .BeTrue();
        }

        private static IEdge<TVertex> InvertEdge<TVertex>(IEdge<TVertex> edge)
        {
            return CreateEdge(edge.Target, edge.Source);
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
