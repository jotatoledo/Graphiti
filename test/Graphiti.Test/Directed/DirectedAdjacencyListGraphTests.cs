// <copyright file="DirectedAdjacencyListGraphTests.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class DirectedAdjacencyListGraphTests
    {
        public static IEnumerable<object[]> Edges => new List<object[]>
        {
            // TODO: once c#@8 lands, the wrap functionality can be deprecated
            // as the ctor method can be invoked by simple using `new`, when the
            // type of the collection is expicitely declared
            new object[] { Enumerable.Empty<TestEdge>() },
            new object[] { new TestEdge[] { TestEdge.Wrap('a', 'b') } },
            new object[] { new TestEdge[] { TestEdge.Wrap('a', 'b'), TestEdge.Wrap('b', 'c'), TestEdge.Wrap('c', 'a') } }
        };

        [Fact]
        public void ItCorrectlyMarksItselfAsDirected()
        {
            var sut = CreateSystemUnderTest();

            sut.IsDirected.Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCalculatesItsMaximumNumberOfEdges(IEnumerable<TestEdge> edges)
        {
            var sut = CreateSystemUnderTest(edges);
            var numberOfVertices = sut.VertexCount;
            var expectedMaxNumberOfEdges = numberOfVertices * (numberOfVertices - 1);

            sut.MaxNumberOfEdges.Should()
                .Be(expectedMaxNumberOfEdges);
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCountsCurrentVertices(IEnumerable<TestEdge> edges)
        {
            var vertices = Utils.ExtractDistinctVertices(edges);
            var sut = CreateSystemUnderTest(edges);

            sut.VertexCount.Should()
                .Be(vertices.Count());
        }

        [Fact]
        public void Should_UpdateCurrentVertices_When_VerticesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var bToC = TestEdge.Wrap('b', 'c');
            var cToD = TestEdge.Wrap('c', 'd');
            var dToA = TestEdge.Wrap('d', 'a');

            TestVertex[] asVertices(params char[] vertices) => vertices.Select(TestVertex.Wrap).ToArray();
            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeVertex(TestVertex v) => sut.RemoveVertex(v);

            AssertCurrentVertices(sut, "because the graph was created without vertices");
            addEdge(aToB);
            AssertCurrentVertices(sut, $"because the vertices of {aToB} were added", asVertices('a', 'b'));
            addEdge(bToC);
            AssertCurrentVertices(sut, $"because the vertices of {bToC} were added", asVertices('a', 'b', 'c'));
            removeVertex(aToB.Target);
            AssertCurrentVertices(sut, $"because {aToB.Target} was removed", asVertices('a', 'c'));
            addEdge(cToD);
            AssertCurrentVertices(sut, $"because the vertices of {cToD} were added", asVertices('a', 'c', 'd'));
            addEdge(dToA);
            AssertCurrentVertices(sut, $"because the vertices of {dToA} were added", asVertices('a', 'c', 'd'));
            removeVertex(dToA.Target);
            AssertCurrentVertices(sut, $"because {dToA.Target} was removed", asVertices('c', 'd'));
            removeVertex(dToA.Source);
            AssertCurrentVertices(sut, $"because {dToA.Source} was removed", asVertices('c'));
            removeVertex(cToD.Source);
            AssertCurrentVertices(sut, $"because {cToD.Source} was removed");
        }

        [Theory]
        [MemberData(nameof(Edges))]
        public void ItCorrectlyCountsCurrentEdges(IEnumerable<TestEdge> edges)
        {
            var sut = CreateSystemUnderTest(edges);

            sut.EdgeCount.Should()
                .Be(edges.Count());
        }

        [Fact]
        public void Should_UpdateCurrentEdges_When_VerticesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var aToC = TestEdge.Wrap('a', 'c');
            var bToC = TestEdge.Wrap('b', 'c');
            var cToD = TestEdge.Wrap('c', 'd');
            var dToA = TestEdge.Wrap('d', 'a');

            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeVertex(TestVertex v) => sut.RemoveVertex(v);

            AssertTrackedEdges(sut, "because the graph was created without edges");
            addEdge(aToB);
            AssertTrackedEdges(sut, $"because {aToB} was added", aToB);
            addEdge(aToC);
            AssertTrackedEdges(sut, $"because {aToC} was added", aToB, aToC);
            addEdge(bToC);
            AssertTrackedEdges(sut, $"because {bToC} was added", aToB, aToC, bToC);
            addEdge(cToD);
            AssertTrackedEdges(sut, $"because {cToD} was added", aToB, aToC, bToC, cToD);
            removeVertex(aToB.Target);
            AssertTrackedEdges(sut, $"because {aToB.Target} was removed", aToC, cToD);
            addEdge(dToA);
            AssertTrackedEdges(sut, $"because {dToA} was added", aToC, cToD, dToA);
            removeVertex(aToC.Source);
            AssertTrackedEdges(sut, $"because {aToC.Source} was removed", cToD);
            removeVertex(cToD.Source);
            AssertTrackedEdges(sut, $"because {cToD.Source} was removed");
        }

        [Fact]
        public void Should_UpdateCurrentEdges_When_EdgesAreMutated()
        {
            var sut = CreateSystemUnderTest();
            var aToB = TestEdge.Wrap('a', 'b');
            var aToC = TestEdge.Wrap('a', 'c');
            var bToC = TestEdge.Wrap('b', 'c');
            var cToA = TestEdge.Wrap('c', 'a');

            void addEdge(TestEdge e) => sut.AddVerticesAndEdge(e);
            void removeEdge(TestEdge e) => sut.RemoveEdge(e);

            AssertTrackedEdges(sut, "because thr graph was created withouth edges");
            addEdge(aToB);
            AssertTrackedEdges(sut, $"because {aToB} was added", aToB);
            addEdge(aToC);
            AssertTrackedEdges(sut, $"because {aToC} was added", aToB, aToC);
            addEdge(bToC);
            AssertTrackedEdges(sut, $"because {bToC} was added", aToB, aToC, bToC);
            removeEdge(TestEdge.Wrap('b', 'a'));
            AssertTrackedEdges(sut, $"because {TestEdge.Wrap('b', 'a')} does not exist", aToB, aToC, bToC);
            addEdge(cToA);
            AssertTrackedEdges(sut, $"because {cToA} was added", aToB, aToC, bToC, cToA);
            removeEdge(aToC);
            AssertTrackedEdges(sut, $"because {aToC} was removed", aToB, bToC, cToA);
            removeEdge(bToC);
            AssertTrackedEdges(sut, $"because {bToC} was removed", aToB, cToA);
            removeEdge(aToB);
            AssertTrackedEdges(sut, $"because {aToB} was removed", cToA);
            removeEdge(cToA);
            AssertTrackedEdges(sut, $"because {aToB} was removed");
        }

        [Fact]
        public void AddingAVertex_Should_NotAddTheVertex_When_VertexAlreadyPresent()
        {
            var vertex = TestVertex.Wrap('a');
            var sut = CreateSystemUnderTest();

            var result = sut.AddVertex(vertex) && sut.AddVertex(vertex);

            result.Should()
                .BeFalse($"because the {vertex} was added 2 times");
            AssertCurrentVertices(sut, "because the vertex was added the first time", vertex);
        }

        [Fact]
        public void AddingAVertex_Should_AddTheVertex_When_VertexNotPresent()
        {
            var vertex = TestVertex.Wrap('a');
            var sut = CreateSystemUnderTest();

            var result = sut.AddVertex(vertex);

            result.Should()
                .BeTrue($"because the {vertex} was not present");
            AssertCurrentVertices(sut, "because the vertex was added the first time", vertex);
        }

        [Fact]
        public void RemovingAVertex_Should_NotRemoveTheVertex_When_VertexNotPresent()
        {
            var vertex = TestVertex.Wrap('a');
            var sut = CreateSystemUnderTest();

            var result = sut.RemoveVertex(vertex);

            result.Should()
                .BeFalse($"because {vertex} is not present in the graph");
            AssertCurrentVertices(sut, "because the graph is empty");
        }

        [Fact]
        public void RemovingAVertex_Should_Work_RemoveTheVertex_VertexPresent()
        {
            var vertex = TestVertex.Wrap('a');
            var sut = CreateSystemUnderTest();

            var result = sut.AddVertex(vertex) && sut.RemoveVertex(vertex);

            result.Should()
                .BeTrue("because the vertex was removed");
            AssertCurrentVertices(sut, "because the graph is empty");
        }

        [Fact]
        public void RemovingAVertex_Should_RemoveInAndOutEdgesOfIt()
        {
            var vertexToRemove = TestVertex.Wrap('a');
            var sut = CreateSystemUnderTest();
            var edges = new TestEdge[]
            {
                TestEdge.Wrap(vertexToRemove.Id, 'b'),
                TestEdge.Wrap('b', 'c'),
                TestEdge.Wrap('c', vertexToRemove.Id)
            };
            sut.AddVerticesAndEdgeRange(edges);

            var removed = sut.RemoveVertex(vertexToRemove);
            var expectedEdges = edges.Where(edge => !(edge.Target.Equals(vertexToRemove) || edge.Source.Equals(vertexToRemove)));
            var expectedVertices = Utils.ExtractDistinctVertices(expectedEdges);

            removed.Should()
                .BeTrue("because the vertex was present");
            AssertCurrentVertices(sut, $"because only {vertexToRemove} was removed", expectedVertices.ToArray());
            AssertTrackedEdges(sut, $"because the in and out edges of {vertexToRemove} were removed", expectedEdges.ToArray());
        }

        [Fact]
        public void OutEdges_Should_Throw_When_VertexNotPresent()
        {
            var vertex = TestVertex.Wrap('c');
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));

            Action outEdges = () => sut.OutEdges(vertex);
            Action outDegree = () => sut.OutDegree(vertex);
            Action hasOutEdges = () => sut.HasOutEdges(vertex);

            outEdges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            outDegree.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            hasOutEdges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
        }

        [Fact]
        public void ItCorrectlyCalculatesOutEdges()
        {
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'c'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'd'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('b', 'c'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('c', 'b'));

            void assertOutEdges(TestVertex vertex, int expectedDegree, string because = null)
            {
                because = because ?? string.Empty;
                sut.HasOutEdges(vertex)
                    .Should()
                    .Be(expectedDegree != 0);
                sut.OutDegree(vertex).Should()
                    .Be(expectedDegree, because);
                sut.OutEdges(vertex)
                    .Should()
                    .HaveCount(expectedDegree)
                    .And
                    .OnlyHaveUniqueItems();
                if (expectedDegree > 0)
                {
                    sut.OutEdges(vertex)
                        .Should()
                        .OnlyContain(edge => edge.Source.Equals(vertex));
                }
            }

            assertOutEdges(TestVertex.Wrap('a'), 3);
            assertOutEdges(TestVertex.Wrap('b'), 1);
            assertOutEdges(TestVertex.Wrap('c'), 1);
            assertOutEdges(TestVertex.Wrap('d'), 0);
        }

        [Fact]
        public void InEdges_Should_Throw_When_VertexNotPresent()
        {
            var vertex = TestVertex.Wrap('c');
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));

            Action inEdges = () => sut.InEdges(vertex);
            Action inDegree = () => sut.InDegree(vertex);
            Action hasInEdges = () => sut.HasInEdges(vertex);

            inEdges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            inDegree.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
            hasInEdges.Should()
                .Throw<VertexNotFoundException<TestVertex>>();
        }

        [Fact]
        public void ItCorrectlyCalculatesInEdges()
        {
            var sut = CreateSystemUnderTest();
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'b'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'c'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('a', 'd'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('b', 'c'));
            sut.AddVerticesAndEdge(TestEdge.Wrap('c', 'b'));

            void assertInEdges(TestVertex vertex, int expectedDegree, string because = null)
            {
                because = because ?? string.Empty;
                sut.HasInEdges(vertex)
                    .Should()
                    .Be(expectedDegree != 0);
                sut.InDegree(vertex).Should()
                    .Be(expectedDegree, because);
                sut.InEdges(vertex)
                    .Should()
                    .HaveCount(expectedDegree)
                    .And
                    .OnlyHaveUniqueItems();
                if (expectedDegree > 0)
                {
                    sut.InEdges(vertex)
                        .Should()
                        .OnlyContain(edge => edge.Target.Equals(vertex));
                }
            }

            assertInEdges(TestVertex.Wrap('a'), 0);
            assertInEdges(TestVertex.Wrap('b'), 2);
            assertInEdges(TestVertex.Wrap('c'), 2);
            assertInEdges(TestVertex.Wrap('d'), 1);
        }

        private static TestDirectedAdjacencyListGraph CreateSystemUnderTest(IEnumerable<TestEdge> edges = null)
        {
            var graph = new TestDirectedAdjacencyListGraph();
            graph.AddVerticesAndEdgeRange(edges ?? Enumerable.Empty<TestEdge>());
            return graph;
        }

        private static void AssertCurrentVertices(TestDirectedAdjacencyListGraph graph, string because, params TestVertex[] expectedVertices)
        {
            graph.HasNoVertices.Should()
                    .Be(!expectedVertices.Any(), because);
            graph.Vertices.Should()
                .BeEquivalentTo(expectedVertices, because);
            graph.VertexCount.Should()
                .Be(expectedVertices.Length, because);
        }

        private static void AssertTrackedEdges(TestDirectedAdjacencyListGraph graph, string because, params TestEdge[] expectedEdges)
        {
            graph.HasNoEdges.Should()
                    .Be(!expectedEdges.Any(), because);
            graph.Edges.Should()
                .BeEquivalentTo(expectedEdges, because);
            graph.EdgeCount.Should()
                .Be(expectedEdges.Length, because);
        }

        private sealed class TestDirectedAdjacencyListGraph : DirectedAdjacencyListGraph<TestVertex, TestEdge>
        {
        }
    }
}
