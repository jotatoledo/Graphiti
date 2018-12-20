// <copyright file="DirectedAdjacencyListGraph.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;
    using System.Linq;
    using Graphiti.Collections;

    /// <summary>
    /// A directed graph based on adjacency lists
    /// </summary>
    /// <typeparam name="TVertex">Type of the verties</typeparam>
    /// <typeparam name="TEdge">Type of the edges</typeparam>
    public class DirectedAdjacencyListGraph<TVertex, TEdge>
        : IMutableVertexAndEdgeSet<TVertex, TEdge>,
        IDirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IVertexEdgeDictionary<TVertex, TEdge> vertexEdges;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedAdjacencyListGraph{TVertex, TEdge}"/> class.
        /// </summary>
        public DirectedAdjacencyListGraph()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedAdjacencyListGraph{TVertex, TEdge}"/> class
        /// with the given initial capacity
        /// </summary>
        /// <param name="capacity">Initial capacity</param>
        public DirectedAdjacencyListGraph(int capacity)
        {
            if (capacity > -1)
            {
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>(capacity);
            }
            else
            {
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>();
            }
        }

        private DirectedAdjacencyListGraph(DirectedAdjacencyListGraph<TVertex, TEdge> graph)
        {
            this.vertexEdges = graph.vertexEdges.Clone();
        }

        /// <inheritdoc/>
        public bool IsDirected => true;

        /// <inheritdoc/>
        public bool HasNoVertices => this.VertexCount == 0;

        /// <inheritdoc/>
        public int VertexCount => this.vertexEdges.Count;

        /// <inheritdoc/>
        public IEnumerable<TVertex> Vertices => this.vertexEdges.Keys;

        /// <inheritdoc/>
        public bool HasNoEdges => this.EdgeCount == 0;

        /// <inheritdoc/>
        public int EdgeCount { get; private set; } = 0;

        /// <inheritdoc/>
        public IEnumerable<TEdge> Edges => this.vertexEdges.Values.SelectMany(edges => edges);

        /// <inheritdoc/>
        public int MaxNumberOfEdges => this.VertexCount * (this.VertexCount - 1);

        /// <inheritdoc/>
        public virtual bool AddEdge(TEdge v)
        {
            var edgeAdded = false;
            if (!this.ContainsEdge(v))
            {
                this.vertexEdges[v.Source].Add(v);
                this.EdgeCount++;
                edgeAdded = true;
            }

            return edgeAdded;
        }

        /// <inheritdoc/>
        public virtual bool AddVertex(TVertex v)
        {
            var added = false;
            if (!this.ContainsVertex(v))
            {
                this.vertexEdges.Add(v, new VertexEdges<TVertex, TEdge>());
                added = true;
            }

            return added;
        }

        /// <inheritdoc/>
        public int AddVertexRange(IEnumerable<TVertex> vertices)
        {
            return vertices.Select(v => this.AddVertex(v))
                .Count(added => added);
        }

        /// <inheritdoc/>
        public bool AddVerticesAndEdge(TEdge edge)
        {
            this.AddVertex(edge.Source);
            this.AddVertex(edge.Target);
            return this.AddEdge(edge);
        }

        /// <inheritdoc/>
        public int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges) => edges
            .Select(this.AddVerticesAndEdge)
            .Count(added => added);

        /// <inheritdoc/>
        public IDirectedGraph<TVertex, TEdge> Clone()
        {
            return new DirectedAdjacencyListGraph<TVertex, TEdge>(this);
        }

        /// <inheritdoc/>
        public bool ContainsEdge(TEdge edge)
        {
            return this.vertexEdges.TryGetValue(edge.Source, out var edges)
                && edges.Contains(edge);
        }

        /// <inheritdoc/>
        public bool ContainsVertex(TVertex vertex)
        {
            return this.vertexEdges.ContainsKey(vertex);
        }

        /// <inheritdoc/>
        public bool HasInEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.InDegree(v) != 0;
        }

        /// <inheritdoc/>
        public bool HasOutEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.OutDegree(v) != 0;
        }

        /// <inheritdoc/>
        public int InDegree(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.InEdges(v).Count();
        }

        /// <inheritdoc/>
        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);

            // TODO revisit for optimizations
            return this.vertexEdges
                .Where(pair => !pair.Key.Equals(v))
                .Select(pair => pair.Value)
                .SelectMany(vertices => vertices.Where(vertex => vertex.Target.Equals(v)));
        }

        /// <inheritdoc/>
        public int OutDegree(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.vertexEdges[v].Count;
        }

        /// <inheritdoc/>
        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.vertexEdges[v];
        }

        /// <inheritdoc/>
        public virtual bool RemoveEdge(TEdge v)
        {
            var removed = false;
            if (this.vertexEdges.TryGetValue(v.Source, out var outEdges)
                && outEdges.Remove(v))
            {
                this.EdgeCount--;
                removed = true;
            }

            return removed;
        }

        /// <inheritdoc/>
        public virtual bool RemoveVertex(TVertex v)
        {
            var removed = false;
            if (this.ContainsVertex(v))
            {
                // Remove out-edges
                var edges = this.vertexEdges[v];
                this.EdgeCount -= edges.Count;
                edges.Clear();

                // Remove before iteration in order to save key-value pair comparision to target vertex
                this.vertexEdges.Remove(v);

                // Remove in-edges
                foreach (var pair in this.vertexEdges)
                {
                    // The vertex edges need to be enumerated otherwise
                    // the enumeration could be mutated during the loop
                    foreach (var edge in pair.Value.ToList())
                    {
                        if (edge.Target.Equals(v))
                        {
                            pair.Value.Remove(edge);
                            this.EdgeCount--;
                        }
                    }
                }

                removed = true;
            }

            return removed;
        }

        private void ThrowIfVertexNotPresent(TVertex v)
        {
            if (!this.ContainsVertex(v))
            {
                throw new VertexNotFoundException<TVertex>(v);
            }
        }
    }
}
