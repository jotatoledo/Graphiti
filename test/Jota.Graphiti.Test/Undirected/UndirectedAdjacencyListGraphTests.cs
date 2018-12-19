// <copyright file="UndirectedAdjacencyListGraphTests.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class UndirectedAdjacencyListGraphTests
    {
        private readonly IEqualityComparer<TestEdge> edgeComparer;

        public UndirectedAdjacencyListGraphTests()
        {
            this.edgeComparer = new UnorderedEdgeComparer<TestVertex, TestEdge>();
        }

        public static IEnumerable<object[]> Edges => new List<object[]>
        {
            // TODO: once c#@8 lands, the wrap functionality can be deprecated
            // as the ctor method can be invoked by simple using `new`, when the
            // type of the collection is expicitely declared
            new object[] { Enumerable.Empty<TestEdge>() },
            new object[] { new TestEdge[] { TestEdge.Wrap('a', 'b') } },
            new object[] { new TestEdge[] { TestEdge.Wrap('a', 'b'), TestEdge.Wrap('b', 'c'), TestEdge.Wrap('c', 'a') } },
            new object[] { new TestEdge[] { TestEdge.Wrap('a', 'b'), TestEdge.Wrap('a', 'b'), TestEdge.Wrap('b', 'c'), TestEdge.Wrap('c', 'a') } }
        };

        [Fact]
        public void ItCorrectlyMarksItselfAsUndirected()
        {
            var sut = CreateSystemUnderTest();

            sut.IsDirected.Should()
                .BeFalse();
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCalculatesItsMaximumNumberOfEdges(IEnumerable<TestEdge> edges)
        {
            var sut = CreateSystemUnderTest(edges);
            var numberOfVertices = sut.VertexCount;
            var expectedMaxNumberOfEdges = numberOfVertices * (numberOfVertices - 1) / 2;

            sut.MaxNumberOfEdges.Should()
                .Be(expectedMaxNumberOfEdges);
        }

        [Fact]
        public void AdjacentEdges_Should_Throw_When_VertexNotPresent()
        {
            var vertex = TestVertex.Wrap('c');
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));

            Action edges = () => sut.AdjacentEdges(vertex);
            Action degree = () => sut.Degree(vertex);
            Action hasEdges = () => sut.HasAdjacentEdges(vertex);

            edges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            degree.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            hasEdges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
        }

        [Fact]
        public void ItCorrectlyCalculatesAdjacentEdges()
        {
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'c'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'd'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('b', 'c'));

            void assertAdjacentEdges(TestVertex vertex, int expectedDegree, string because = null)
            {
                because = because ?? string.Empty;
                sut.HasAdjacentEdges(vertex)
                    .Should()
                    .Be(expectedDegree > 0, because);
                sut.AdjacentEdges(vertex)
                    .Should()
                    .HaveCount(expectedDegree, because)
                    .And
                    .OnlyHaveUniqueItems(because);
                if (expectedDegree > 0)
                {
                    sut.AdjacentEdges(vertex)
                        .Should()
                        .OnlyContain(edge => edge.Target.Equals(vertex) || edge.Source.Equals(vertex), because);
                }
            }

            assertAdjacentEdges(TestVertex.Wrap('a'), 3);
            assertAdjacentEdges(TestVertex.Wrap('b'), 2);
            assertAdjacentEdges(TestVertex.Wrap('c'), 2);
            assertAdjacentEdges(TestVertex.Wrap('d'), 1);
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCountsCurrentEdges(IEnumerable<TestEdge> edges)
        {
            var sut = CreateSystemUnderTest(edges);
            var uniqueEdges = new HashSet<TestEdge>(edges, this.edgeComparer);

            sut.EdgeCount.Should()
                .Be(uniqueEdges.Count());
        }

        [Fact]
        public void Should_UpdateCurrentEdges_When_EdgesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var aToC = TestEdge.Wrap('a', 'c');

            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeEdge(TestEdge e) => sut.RemoveEdge(e);

            AssertCurrentEdges(sut, "because thr graph was created withouth edges");
            addEdge(aToB);
            AssertCurrentEdges(sut, $"because {aToB} was added", aToB);
            addEdge(aToB.Invert() as TestEdge);
            AssertCurrentEdges(sut, $"because inverted {aToB} wont be added", aToB);
            addEdge(aToC);
            AssertCurrentEdges(sut, $"because {aToC} was added", aToB, aToC);
            removeEdge(aToB);
            AssertCurrentEdges(sut, $"because {aToB} was removed", aToC);
            removeEdge(aToC);
            AssertCurrentEdges(sut, $"because {aToC} was removed");
        }

        [Fact]
        public void Should_UpdateCurrentEdges_When_VerticesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var aToC = TestEdge.Wrap('a', 'c');
            var bToD = TestEdge.Wrap('b', 'd');
            var cToD = TestEdge.Wrap('c', 'd');

            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeVertex(TestVertex v) => sut.RemoveVertex(v);

            AssertCurrentEdges(sut, "because thr graph was created withouth edges");
            addEdge(aToB);
            AssertCurrentEdges(sut, $"because {aToB} was added", aToB);
            addEdge(bToD);
            AssertCurrentEdges(sut, $"because {bToD} was added", aToB, bToD);
            addEdge(aToC);
            AssertCurrentEdges(sut, $"because {aToC} was added", aToB, bToD, aToC);
            removeVertex(bToD.Source);
            AssertCurrentEdges(sut, $"because vertex {bToD.Source} was removed", aToC);
            addEdge(cToD);
            AssertCurrentEdges(sut, $"because {cToD} was added", aToC, cToD);
            removeVertex(cToD.Source);
            AssertCurrentEdges(sut, $"because vertex {cToD.Source} was removed");
        }

        [Fact]
        public void Should_NotAddEdge_When_InvertedValueAlreadyExist()
        {
            var edge = TestEdge.Wrap('a', 'b');
            var sut = CreateSystemUnderTest(new[] { edge });

            var result = sut.AddEdge(edge.Invert() as TestEdge);

            result.Should().BeFalse($"because {edge} was already added");
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCountsCurrentVertices(IEnumerable<TestEdge> edges)
        {
            var sut = CreateSystemUnderTest(edges);
            var uniqueVertices = Utils.ExtractDistinctVertices(edges);

            sut.VertexCount.Should()
                .Be(uniqueVertices.Count());
        }

        [Fact]
        public void Should_UpdateCurrentVertices_When_VerticesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var bToC = TestEdge.Wrap('b', 'c');
            var cToD = TestEdge.Wrap('c', 'd');

            TestVertex[] asVertices(params char[] vertices) => vertices.Select(TestVertex.Wrap).ToArray();
            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeVertex(TestVertex v) => sut.RemoveVertex(v);

            AssertCurrentVertices(sut, "because the graph was created without vertices");
            addEdge(aToB);
            AssertCurrentVertices(sut, $"because {aToB} was added", asVertices('a', 'b'));
            addEdge(bToC);
            AssertCurrentVertices(sut, $"because {bToC} was added", asVertices('a', 'b', 'c'));
            removeVertex(aToB.Target);
            AssertCurrentVertices(sut, $"because {aToB.Target} was removed", asVertices('a', 'c'));
            addEdge(cToD);
            AssertCurrentVertices(sut, $"because {cToD} was added", asVertices('a', 'c', 'd'));
            addEdge(cToD.Invert() as TestEdge);
            AssertCurrentVertices(sut, $"because {cToD.Invert()} had no effects", asVertices('a', 'c', 'd'));
            removeVertex(TestVertex.Wrap('a'));
            AssertCurrentVertices(sut, $"because {TestVertex.Wrap('a')} was removed", asVertices('c', 'd'));
            removeVertex(TestVertex.Wrap('c'));
            AssertCurrentVertices(sut, $"because {TestVertex.Wrap('c')} was removed", asVertices('d'));
            removeVertex(TestVertex.Wrap('d'));
            AssertCurrentVertices(sut, $"because {TestVertex.Wrap('d')} was removed");
        }

        private static void AssertCurrentVertices(TestUndirectedAdjacencyListGraph graph, string because, params TestVertex[] expectedVertices)
        {
            graph.HasNoVertices.Should()
                .Be(!expectedVertices.Any(), because);
            graph.Vertices.Should()
                .BeEquivalentTo(expectedVertices, because);
            graph.VertexCount.Should()
                .Be(expectedVertices.Length, because);
        }

        private static void AssertCurrentEdges(TestUndirectedAdjacencyListGraph graph, string because, params TestEdge[] expectedEdges)
        {
            graph.HasNoEdges.Should()
                .Be(!expectedEdges.Any(), because);
            graph.Edges.Should()
                .BeEquivalentTo(expectedEdges, because);
            graph.EdgeCount.Should()
                .Be(expectedEdges.Length, because);
        }

        private static TestUndirectedAdjacencyListGraph CreateSystemUnderTest(IEnumerable<TestEdge> edges = null)
        {
            var graph = new TestUndirectedAdjacencyListGraph();
            graph.AddVerticesAndEdgeRange(edges ?? Enumerable.Empty<TestEdge>());
            return graph;
        }

        private sealed class TestUndirectedAdjacencyListGraph : UndirectedAdjacencyListGraph<TestVertex, TestEdge>
        {
        }
    }
}
