// <copyright file="IGraph.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    /// <summary>
    /// A graph structure with vertices of type <typeparamref name="TVertex"/>
    /// and edges of type <typeparamref name="TEdge"/>
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices</typeparam>
    /// <typeparam name="TEdge">The type of the edges</typeparam>
    public interface IGraph<TVertex, TEdge>
        : IVertexSet<TVertex>,
        IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether the graph is directed or not
        /// </summary>
        bool IsDirected { get; }

        /// <summary>
        /// Gets the maximum number of edges that the graph can contain
        /// </summary>
        int MaxNumberOfEdges { get; }
    }
}
