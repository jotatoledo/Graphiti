// <copyright file="VertexEdgeDictionaryTests.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti.Test.Collections
{
    using System.Linq;
    using FluentAssertions;
    using Jota.Graphiti.Collections;
    using Xunit;

    public class VertexEdgeDictionaryTests
    {
        [Fact]
        public void Clone_Should_CorrectlyRun()
        {
            var sut = this.CreateSystemUnderTest<char, Edge<char>>();
            sut.Add('a', CreateEdgeSet<char, Edge<char>>(new Edge<char>('a', 'b')));
            sut.Add('b', CreateEdgeSet<char, Edge<char>>(new Edge<char>('b', 'c'), new Edge<char>('a', 'b')));

            var clone = sut.Clone();

            clone.Should()
                .NotBeSameAs(sut, "because a new instance was created")
                .And
                .BeEquivalentTo(clone, "because the keys and associated vertex sets were copied");
            clone.Values.Should()
                .OnlyContain(clonedSet => !CloneSetPresentInSourceValues(clonedSet, sut), "because the mapped sets were (shallow) copied");
        }

        private static IVertexEdges<TVertex, TEdge> CreateEdgeSet<TVertex, TEdge>(params TEdge[] edges)
            where TEdge : IEdge<TVertex>
        {
            var set = new VertexEdges<TVertex, TEdge>();
            foreach (var edge in edges)
            {
                set.Add(edge);
            }

            return set;
        }

        private static bool CloneSetPresentInSourceValues<TVertex, TEdge>(
                IVertexEdges<TVertex, TEdge> cloneSet,
                IVertexEdgeDictionary<TVertex, TEdge> dict)
                where TEdge : IEdge<TVertex> => dict.Values.Any(sourceSet => ReferenceEquals(sourceSet, cloneSet));

        private VertexEdgeDictionary<TVertex, TEdge> CreateSystemUnderTest<TVertex, TEdge>()
            where TEdge : IEdge<TVertex> => new VertexEdgeDictionary<TVertex, TEdge>();
    }
}
