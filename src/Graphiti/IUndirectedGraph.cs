// <copyright file="IUndirectedGraph.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// Behavior contract for an undirected graph
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    public interface IUndirectedGraph<TVertex, TEdge>
        : IGraph<TVertex, TEdge>,
        IShallowCloneable<IUndirectedGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets the adjacent edges to a vertex
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>An enumeration with the adjacent edges</returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        IEnumerable<TEdge> AdjacentEdges(TVertex v);

        /// <summary>
        /// Determines whether there are adjacent edges associated to <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>
        /// <c>true</c> if <paramref name="v"/> has no adjacent edges; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        bool HasAdjacentEdges(TVertex v);

        /// <summary>
        /// Gets the count of adjacent edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>The count of adjacent edges of <paramref name="v"/></returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        int Degree(TVertex v);
    }
}
