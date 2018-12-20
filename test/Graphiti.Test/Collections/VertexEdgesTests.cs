// <copyright file="VertexEdgesTests.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Test.Collections
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Graphiti.Collections;
    using Xunit;

    public class VertexEdgesTests
    {
        [Fact]
        public void Clone_Should_CorrectlyRun()
        {
            var comparer = EqualityComparer<Edge<int>>.Default;
            var sut = this.CreateSystemUnderTest<int, Edge<int>>(comparer);
            sut.Add(new Edge<int>(1, 2));
            sut.Add(new Edge<int>(3, 4));

            var clone = sut.Clone();

            clone.Should()
                .NotBeSameAs(sut)
                .And
                .HaveSameCount(sut)
                .And
                .BeEquivalentTo(sut);
            clone.EdgeComparer.Should()
                .Be(sut.EdgeComparer);
        }

        private VertexEdges<TVertex, TEdge> CreateSystemUnderTest<TVertex, TEdge>(IEqualityComparer<TEdge> comparer)
            where TEdge : IEdge<TVertex>
        {
            return new VertexEdges<TVertex, TEdge>(comparer);
        }
    }
}
