// <copyright file="UndirectedAdjacencyListGraph.cs" company="Graphiti">
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
    /// A mutable undirected graph data structure
    /// </summary>
    /// <typeparam name="TVertex">Type of the vertices</typeparam>
    /// <typeparam name="TEdge">Type of the edges</typeparam>
    public class UndirectedAdjacencyListGraph<TVertex, TEdge>
        : IMutableVertexAndEdgeSet<TVertex, TEdge>,
        IUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IEqualityComparer<TEdge> edgeComparer;
        private readonly IVertexEdgeDictionary<TVertex, TEdge> vertexEdges;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndirectedAdjacencyListGraph{TVertex, TEdge}"/> class
        /// with the default initial capacity
        /// </summary>
        public UndirectedAdjacencyListGraph()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndirectedAdjacencyListGraph{TVertex, TEdge}"/> class
        /// with the given initial capacity
        /// </summary>
        /// <param name="capacity">The capacity</param>
        public UndirectedAdjacencyListGraph(int capacity)
        {
            if (capacity > -1)
            {
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>(capacity);
            }
            else
            {
                this.vertexEdges = new VertexEdgeDictionary<TVertex, TEdge>();
            }

            this.edgeComparer = new UnorderedEdgeComparer<TVertex, TEdge>();
        }

        private UndirectedAdjacencyListGraph(UndirectedAdjacencyListGraph<TVertex, TEdge> graph)
        {
            this.vertexEdges = graph.vertexEdges.Clone();
            this.edgeComparer = new UnorderedEdgeComparer<TVertex, TEdge>();
        }

        /// <inheritdoc/>
        public bool HasNoEdges => this.EdgeCount == 0;

        /// <inheritdoc/>
        public int EdgeCount { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<TEdge> Edges => this.vertexEdges.SelectMany(pair => pair.Value).Distinct(this.edgeComparer);

        /// <inheritdoc/>
        public bool HasNoVertices => this.VertexCount == 0;

        /// <inheritdoc/>
        public int VertexCount => this.vertexEdges.Count;

        /// <inheritdoc/>
        public IEnumerable<TVertex> Vertices => this.vertexEdges.Keys;

        /// <inheritdoc/>
        public bool IsDirected => false;

        /// <inheritdoc/>
        public int MaxNumberOfEdges => this.VertexCount * (this.VertexCount - 1) / 2;

        /// <inheritdoc/>
        public virtual bool AddEdge(TEdge v)
        {
            var added = false;
            if (!this.ContainsEdge(v))
            {
                this.vertexEdges[v.Source].Add(v);
                this.vertexEdges[v.Target].Add(v);
                this.EdgeCount++;
                added = true;
            }

            return added;
        }

        /// <inheritdoc/>
        public virtual bool AddVertex(TVertex v)
        {
            var added = false;
            if (!this.ContainsVertex(v))
            {
                this.vertexEdges.Add(v, this.CreateVertexEdges());
                added = true;
            }

            return added;
        }

        /// <inheritdoc/>
        public int AddVertexRange(IEnumerable<TVertex> vertices)
        {
            return vertices.Select(v => this.AddVertex(v))
                .Where(added => added)
                .Count();
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
        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.vertexEdges[v];
        }

        /// <inheritdoc/>
        public IUndirectedGraph<TVertex, TEdge> Clone()
        {
            return new UndirectedAdjacencyListGraph<TVertex, TEdge>(this);
        }

        /// <inheritdoc/>
        public bool ContainsEdge(TEdge edge) => this.vertexEdges.TryGetValue(edge.Source, out var edges)
            && edges.Contains(edge);

        /// <inheritdoc/>
        public bool ContainsVertex(TVertex vertex)
        {
            return this.vertexEdges.ContainsKey(vertex);
        }

        /// <inheritdoc/>
        public int Degree(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.vertexEdges[v].Count;
        }

        /// <inheritdoc/>
        public bool HasAdjacentEdges(TVertex v)
        {
            this.ThrowIfVertexNotPresent(v);
            return this.Degree(v) != 0;
        }

        /// <inheritdoc/>
        public virtual bool RemoveEdge(TEdge v)
        {
            var removed = false;
            if (this.ContainsEdge(v))
            {
                this.vertexEdges[v.Source].Remove(v);
                this.vertexEdges[v.Target].Remove(v);
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
                // Remove directly associated adjacent edges
                var adjacentEdges = this.vertexEdges[v];
                this.EdgeCount -= adjacentEdges.Count;
                TVertex pickNotRemovedVertex(TEdge edge) => edge.Source.Equals(v) ? edge.Target : edge.Source;

                // Remove the adjacent edges from the
                // edge sets associated to the edge target
                foreach (var edge in adjacentEdges)
                {
                    this.vertexEdges[pickNotRemovedVertex(edge)].Remove(edge);
                }

                this.vertexEdges.Remove(v);
                adjacentEdges.Clear();
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

        private VertexEdges<TVertex, TEdge> CreateVertexEdges()
        {
            return new VertexEdges<TVertex, TEdge>(this.edgeComparer);
        }
    }
}
