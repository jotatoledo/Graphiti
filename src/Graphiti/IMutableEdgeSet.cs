// <copyright file="IMutableEdgeSet.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    /// <summary>
    /// A mutable set of edges
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices</typeparam>
    /// <typeparam name="TEdge">The type of edges</typeparam>
    public interface IMutableEdgeSet<TVertex, TEdge> : IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Adds an edge
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>
        /// <c>true</c> if the edge was added; <c>false</c> otherwise
        /// </returns>
        bool AddEdge(TEdge v);

        /// <summary>
        /// Removes an edge
        /// </summary>
        /// <param name="v">The edge</param>
        /// <returns>
        /// <c>true</c> if the edge was removed; <c>false</c> otherwise
        /// </returns>
        bool RemoveEdge(TEdge v);
    }
}
