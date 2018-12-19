// <copyright file="IMutableVertexAndEdgeSet.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// A mutable vertex and edge set
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    public interface IMutableVertexAndEdgeSet<TVertex, TEdge>
        : IVertexAndEdgeSet<TVertex, TEdge>,
        IMutableEdgeSet<TVertex, TEdge>,
        IMutableVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Adds an edge, and its vertices if necessary
        /// </summary>
        /// <param name="edge">The edge</param>
        /// <returns>true if the edge was added, otherwise false.</returns>
        bool AddVerticesAndEdge(TEdge edge);

        /// <summary>
        /// Adds a collection of edges, and it's vertices if necessary
        /// </summary>
        /// <param name="edges">The edges</param>
        /// <returns>the number of edges added.</returns>
        int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges);
    }
}
